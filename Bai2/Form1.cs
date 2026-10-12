using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Bai2.Models;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;

namespace Bai2
{
    public partial class Form1 : Form
    {
        public static readonly CultureInfo VnCulture = new CultureInfo("vi-VN")
        {
            NumberFormat =
            {
                NumberGroupSeparator = ".",
                NumberDecimalSeparator = ","
            }
        };

        private int _currentMaHd = 0;
        private bool _isBindingUi = false;

        public Form1()
        {
            InitializeComponent();
            SetupDataGridViews();
        }

        private void SetupDataGridViews()
        {
            // Thiết lập DataGridView Hóa đơn (lưới trái)
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.Columns.Clear();

            var colHdMaVal = new DataGridViewTextBoxColumn
            {
                Name = "colHdMaVal",
                DataPropertyName = "MaHd",
                Visible = false
            };
            var colHdMa = new DataGridViewTextBoxColumn
            {
                Name = "colHdMa",
                DataPropertyName = "MaHdHienThi",
                HeaderText = "Mã HD",
                Width = 90
            };
            var colHdNgay = new DataGridViewTextBoxColumn
            {
                Name = "colHdNgay",
                DataPropertyName = "NgayLapHienThi",
                HeaderText = "Ngày lập",
                Width = 125
            };
            var colHdBan = new DataGridViewTextBoxColumn
            {
                Name = "colHdBan",
                DataPropertyName = "SoBan",
                HeaderText = "Số bàn",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colHdHinhThuc = new DataGridViewTextBoxColumn
            {
                Name = "colHdHinhThuc",
                DataPropertyName = "HinhThucTt",
                HeaderText = "Hình thức TT",
                Width = 100
            };
            var colHdTotal = new DataGridViewTextBoxColumn
            {
                Name = "colHdTotal",
                DataPropertyName = "TongTienHienThi",
                HeaderText = "Tổng tiền",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };

            dgvHoaDon.Columns.AddRange(colHdMaVal, colHdMa, colHdNgay, colHdBan, colHdHinhThuc, colHdTotal);

            // Thiết lập DataGridView Chi tiết món (lưới phải)
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.Columns.Clear();

            var colCtMaVal = new DataGridViewTextBoxColumn
            {
                Name = "colCtMaVal",
                DataPropertyName = "MaCt",
                Visible = false
            };
            var colCtMonVal = new DataGridViewTextBoxColumn
            {
                Name = "colCtMonVal",
                DataPropertyName = "MaMon",
                Visible = false
            };
            var colCtStt = new DataGridViewTextBoxColumn
            {
                Name = "colCtStt",
                DataPropertyName = "STT",
                HeaderText = "STT",
                Width = 45,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colCtTen = new DataGridViewTextBoxColumn
            {
                Name = "colCtTen",
                DataPropertyName = "TenMon",
                HeaderText = "Tên món",
                Width = 180
            };
            var colCtSl = new DataGridViewTextBoxColumn
            {
                Name = "colCtSl",
                DataPropertyName = "SoLuong",
                HeaderText = "SL",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colCtGia = new DataGridViewTextBoxColumn
            {
                Name = "colCtGia",
                DataPropertyName = "DonGiaHienThi",
                HeaderText = "Đơn giá",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            var colCtThanhTien = new DataGridViewTextBoxColumn
            {
                Name = "colCtThanhTien",
                DataPropertyName = "ThanhTienHienThi",
                HeaderText = "Thành tiền",
                Width = 135,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };

            dgvChiTiet.Columns.AddRange(colCtMaVal, colCtMonVal, colCtStt, colCtTen, colCtSl, colCtGia, colCtThanhTien);

            // Sự kiện chọn dòng
            dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;
            dgvChiTiet.SelectionChanged += DgvChiTiet_SelectionChanged;

            // Gán các sự kiện click cho các nút
            btnTim.Click += BtnTim_Click;
            txtTimTenMon.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnTim_Click(s, e);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
            btnThemMon.Click += BtnThemMon_Click;
            btnHoaDonMoi.Click += BtnHoaDonMoi_Click;
            btnSuaHoaDon.Click += BtnSuaHoaDon_Click;
            btnXoaHoaDon.Click += BtnXoaHoaDon_Click;
            btnSuaSL.Click += BtnSuaSL_Click;
            btnXoaMon.Click += BtnXoaMon_Click;
            btnXemHoaDon.Click += BtnXemHoaDon_Click;
            btnXuatPdf.Click += BtnXuatPdf_Click;
            btnIn.Click += BtnIn_Click;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // Cài đặt bộ lọc Tìm kiếm
            dtpTuNgay.Value = new DateTime(2026, 10, 1);
            dtpDenNgay.Value = new DateTime(2026, 10, 31);

            cboTimSoBan.Items.Clear();
            cboTimSoBan.Items.Add("Tất cả");
            for (int i = 1; i <= 10; i++)
            {
                cboTimSoBan.Items.Add(i.ToString());
            }
            cboTimSoBan.SelectedIndex = 0;

            cboTimHinhThuc.Items.Clear();
            cboTimHinhThuc.Items.AddRange(new object[] { "Tất cả", "Tiền mặt", "Chuyển khoản" });
            cboTimHinhThuc.SelectedIndex = 0;

            // Cài đặt thông tin chi tiết
            cboSoBan.Items.Clear();
            for (int i = 1; i <= 10; i++)
            {
                cboSoBan.Items.Add(i.ToString());
            }
            cboSoBan.SelectedIndex = 0;

            dtpNgayLap.Value = DateTime.Now;

            LoadDanhSachMon();
            LoadDanhSachHoaDon();
        }

        private void LoadDanhSachMon()
        {
            try
            {
                using var db = new QuanCaPheContext();
                var danhSachMon = db.Mons
                    .OrderBy(m => m.MaMon)
                    .Select(m => new MonComboboxItem
                    {
                        MaMon = m.MaMon,
                        TenMon = m.TenMon ?? "",
                        DonGia = m.DonGia ?? 0
                    })
                    .ToList();

                cboMon.DataSource = danhSachMon;
                cboMon.DisplayMember = "DisplayText";
                cboMon.ValueMember = "MaMon";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachHoaDon(int? selectMaHd = null, bool keepSelection = true)
        {
            try
            {
                int targetMaHd = selectMaHd ?? (_currentMaHd > 0 && keepSelection ? _currentMaHd : 0);

                using var db = new QuanCaPheContext();
                var query = db.HoaDons
                    .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(ct => ct.MaMonNavigation)
                    .AsNoTracking()
                    .AsQueryable();

                // Lọc ngày
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay);

                // Lọc số bàn
                if (cboTimSoBan.SelectedIndex > 0 && int.TryParse(cboTimSoBan.SelectedItem?.ToString(), out int soBan))
                {
                    query = query.Where(h => h.SoBan == soBan);
                }

                // Lọc hình thức
                if (cboTimHinhThuc.SelectedIndex > 0)
                {
                    string hinhThuc = cboTimHinhThuc.SelectedItem?.ToString() ?? "";
                    query = query.Where(h => h.HinhThucTt == hinhThuc);
                }

                // Lọc tên món
                string keyword = txtTimTenMon.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(h => h.ChiTietHoaDons.Any(ct =>
                        ct.MaMonNavigation != null && ct.MaMonNavigation.TenMon.ToLower().Contains(keyword)));
                }

                var rawList = query.OrderBy(h => h.MaHd).ToList();

                var listItems = rawList.Select(h =>
                {
                    decimal tongTien = h.ChiTietHoaDons?.Sum(ct => (ct.SoLuong ?? 0) * (ct.DonGia ?? 0)) ?? 0;
                    return new HoaDonViewItem
                    {
                        MaHd = h.MaHd,
                        NgayLap = h.NgayLap,
                        SoBan = h.SoBan,
                        HinhThucTt = h.HinhThucTt ?? "Tiền mặt",
                        GhiChu = h.GhiChu,
                        TongTien = tongTien
                    };
                }).ToList();

                _isBindingUi = true;
                dgvHoaDon.DataSource = listItems;
                _isBindingUi = false;

                // Cập nhật tổng doanh thu
                decimal tongDoanhThu = listItems.Sum(x => x.TongTien);
                lblTongDoanhThu.Text = $"Tổng doanh thu đang hiển thị: {tongDoanhThu.ToString("#,##0", VnCulture)} đ";

                // Chọn dòng
                if (listItems.Count > 0)
                {
                    int rowIndexToSelect = 0;
                    if (targetMaHd > 0)
                    {
                        int foundIndex = listItems.FindIndex(x => x.MaHd == targetMaHd);
                        if (foundIndex >= 0) rowIndexToSelect = foundIndex;
                    }

                    dgvHoaDon.ClearSelection();
                    dgvHoaDon.Rows[rowIndexToSelect].Selected = true;
                    dgvHoaDon.CurrentCell = dgvHoaDon.Rows[rowIndexToSelect].Cells["colHdMa"];
                    HienThiChiTietHoaDon(listItems[rowIndexToSelect].MaHd);
                }
                else
                {
                    _currentMaHd = 0;
                    gbChiTiet.Text = "Chi tiết hóa đơn";
                    dgvChiTiet.DataSource = null;
                    lblTongCong.Text = "Tổng cộng: 0 đ";
                    txtGhiChu.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiChiTietHoaDon(int maHd)
        {
            _currentMaHd = maHd;
            try
            {
                using var db = new QuanCaPheContext();
                var hd = db.HoaDons
                    .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(ct => ct.MaMonNavigation)
                    .AsNoTracking()
                    .FirstOrDefault(h => h.MaHd == maHd);

                if (hd == null)
                {
                    gbChiTiet.Text = "Chi tiết hóa đơn";
                    dgvChiTiet.DataSource = null;
                    lblTongCong.Text = "Tổng cộng: 0 đ";
                    return;
                }

                // Cập nhật thông tin vùng chi tiết
                gbChiTiet.Text = $"Chi tiết hóa đơn HD{hd.MaHd:D6}";
                dtpNgayLap.Value = hd.NgayLap ?? DateTime.Now;

                string banStr = (hd.SoBan ?? 1).ToString();
                int idxBan = cboSoBan.FindStringExact(banStr);
                if (idxBan >= 0) cboSoBan.SelectedIndex = idxBan;

                if (hd.HinhThucTt == "Chuyển khoản")
                {
                    radChuyenKhoan.Checked = true;
                }
                else
                {
                    radTienMat.Checked = true;
                }

                txtGhiChu.Text = hd.GhiChu ?? "";

                // Nạp chi tiết món
                var chiTietList = (hd.ChiTietHoaDons ?? new List<ChiTietHoaDon>())
                    .OrderBy(ct => ct.MaCt)
                    .Select((ct, index) => new ChiTietViewItem
                    {
                        MaCt = ct.MaCt,
                        MaMon = ct.MaMon ?? 0,
                        STT = index + 1,
                        TenMon = ct.MaMonNavigation?.TenMon ?? "",
                        SoLuong = ct.SoLuong ?? 0,
                        DonGia = ct.DonGia ?? 0
                    })
                    .ToList();

                _isBindingUi = true;
                dgvChiTiet.DataSource = chiTietList;
                _isBindingUi = false;

                decimal tongCong = chiTietList.Sum(ct => ct.ThanhTien);
                lblTongCong.Text = $"Tổng cộng: {tongCong.ToString("#,##0", VnCulture)} đ";

                if (chiTietList.Count > 0)
                {
                    dgvChiTiet.ClearSelection();
                    dgvChiTiet.Rows[0].Selected = true;
                    dgvChiTiet.CurrentCell = dgvChiTiet.Rows[0].Cells["colCtTen"];
                    nudSoLuong.Value = Math.Clamp(chiTietList[0].SoLuong, nudSoLuong.Minimum, nudSoLuong.Maximum);
                }
                else
                {
                    nudSoLuong.Value = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHoaDon_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isBindingUi) return;

            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                var row = dgvHoaDon.SelectedRows[0];
                if (row.DataBoundItem is HoaDonViewItem item)
                {
                    HienThiChiTietHoaDon(item.MaHd);
                }
            }
        }

        private void DgvChiTiet_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isBindingUi) return;

            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                var row = dgvChiTiet.SelectedRows[0];
                if (row.DataBoundItem is ChiTietViewItem item)
                {
                    nudSoLuong.Value = Math.Clamp(item.SoLuong, nudSoLuong.Minimum, nudSoLuong.Maximum);
                    cboMon.SelectedValue = item.MaMon;
                }
            }
        }

        private void BtnTim_Click(object? sender, EventArgs e)
        {
            LoadDanhSachHoaDon(keepSelection: false);
        }

        private void BtnHoaDonMoi_Click(object? sender, EventArgs e)
        {
            try
            {
                using var db = new QuanCaPheContext();
                int soBan = int.TryParse(cboSoBan.SelectedItem?.ToString(), out int sb) ? sb : 1;
                string hinhThuc = radChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
                string? ghiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

                var hd = new HoaDon
                {
                    NgayLap = dtpNgayLap.Value,
                    SoBan = soBan,
                    HinhThucTt = hinhThuc,
                    GhiChu = ghiChu
                };

                db.HoaDons.Add(hd);
                db.SaveChanges();

                MessageBox.Show($"Đã tạo hóa đơn mới HD{hd.MaHd:D6}! Bây giờ bạn có thể chọn món và nhấn 'Thêm món'.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDanhSachHoaDon(selectMaHd: hd.MaHd);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo hóa đơn mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaHoaDon_Click(object? sender, EventArgs e)
        {
            if (_currentMaHd == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new QuanCaPheContext();
                var hd = db.HoaDons.Find(_currentMaHd);
                if (hd == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                hd.NgayLap = dtpNgayLap.Value;
                hd.SoBan = int.TryParse(cboSoBan.SelectedItem?.ToString(), out int sb) ? sb : 1;
                hd.HinhThucTt = radChuyenKhoan.Checked ? "Chuyển khoản" : "Tiền mặt";
                hd.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

                db.SaveChanges();

                MessageBox.Show($"Cập nhật thông tin hóa đơn HD{hd.MaHd:D6} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachHoaDon(keepSelection: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaHoaDon_Click(object? sender, EventArgs e)
        {
            if (_currentMaHd == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa hóa đơn HD{_currentMaHd:D6} cùng toàn bộ các món trong hóa đơn?",
                "Xác nhận xóa hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var db = new QuanCaPheContext();
                var hd = db.HoaDons.Include(h => h.ChiTietHoaDons).FirstOrDefault(h => h.MaHd == _currentMaHd);
                if (hd != null)
                {
                    db.ChiTietHoaDons.RemoveRange(hd.ChiTietHoaDons);
                    db.HoaDons.Remove(hd);
                    db.SaveChanges();

                    MessageBox.Show("Đã xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachHoaDon(keepSelection: false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThemMon_Click(object? sender, EventArgs e)
        {
            if (_currentMaHd == 0)
            {
                MessageBox.Show("Vui lòng chọn hoặc tạo hóa đơn trước khi thêm món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboMon.SelectedItem is not MonComboboxItem selectedMon)
            {
                MessageBox.Show("Vui lòng chọn món cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuong = (int)nudSoLuong.Value;
            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new QuanCaPheContext();
                var mon = db.Mons.Find(selectedMon.MaMon);
                if (mon == null)
                {
                    MessageBox.Show("Không tìm thấy món này trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal donGiaHienTai = mon.DonGia ?? 0;

                // Nếu món đã có trong hóa đơn thì cộng dồn số lượng
                var ct = db.ChiTietHoaDons.FirstOrDefault(c => c.MaHd == _currentMaHd && c.MaMon == mon.MaMon);
                if (ct != null)
                {
                    ct.SoLuong = (ct.SoLuong ?? 0) + soLuong;
                }
                else
                {
                    ct = new ChiTietHoaDon
                    {
                        MaHd = _currentMaHd,
                        MaMon = mon.MaMon,
                        SoLuong = soLuong,
                        DonGia = donGiaHienTai
                    };
                    db.ChiTietHoaDons.Add(ct);
                }

                db.SaveChanges();

                // Cập nhật lại giao diện
                HienThiChiTietHoaDon(_currentMaHd);
                LoadDanhSachHoaDon(keepSelection: true);
                nudSoLuong.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuaSL_Click(object? sender, EventArgs e)
        {
            if (_currentMaHd == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvChiTiet.SelectedRows.Count == 0 || dgvChiTiet.SelectedRows[0].DataBoundItem is not ChiTietViewItem selectedItem)
            {
                MessageBox.Show("Vui lòng chọn món cần sửa số lượng trong danh sách chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soLuongMoi = (int)nudSoLuong.Value;
            if (soLuongMoi <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var db = new QuanCaPheContext();
                var ct = db.ChiTietHoaDons.Find(selectedItem.MaCt);
                if (ct != null)
                {
                    ct.SoLuong = soLuongMoi;
                    db.SaveChanges();

                    HienThiChiTietHoaDon(_currentMaHd);
                    LoadDanhSachHoaDon(keepSelection: true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa số lượng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaMon_Click(object? sender, EventArgs e)
        {
            if (_currentMaHd == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvChiTiet.SelectedRows.Count == 0 || dgvChiTiet.SelectedRows[0].DataBoundItem is not ChiTietViewItem selectedItem)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa trong danh sách chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có muốn xóa món '{selectedItem.TenMon}' khỏi hóa đơn này không?",
                "Xác nhận xóa món", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var db = new QuanCaPheContext();
                var ct = db.ChiTietHoaDons.Find(selectedItem.MaCt);
                if (ct != null)
                {
                    db.ChiTietHoaDons.Remove(ct);
                    db.SaveChanges();

                    HienThiChiTietHoaDon(_currentMaHd);
                    LoadDanhSachHoaDon(keepSelection: true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa món: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region FastReport In & Xuất PDF

        private Report? PrepareReport(out string error)
        {
            error = string.Empty;

            if (_currentMaHd == 0)
            {
                error = "Vui lòng chọn một hóa đơn!";
                return null;
            }

            using var db = new QuanCaPheContext();
            var hd = db.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.MaMonNavigation)
                .AsNoTracking()
                .FirstOrDefault(h => h.MaHd == _currentMaHd);

            if (hd == null)
            {
                error = "Không tìm thấy hóa đơn cần in trong cơ sở dữ liệu!";
                return null;
            }

            if (hd.ChiTietHoaDons == null || !hd.ChiTietHoaDons.Any())
            {
                error = "Hóa đơn này chưa có món nào, không thể in trang trống!";
                return null;
            }

            var listChiTiet = hd.ChiTietHoaDons
                .OrderBy(ct => ct.MaCt)
                .Select((ct, idx) => new ChiTietIn
                {
                    STT = idx + 1,
                    TenMon = ct.MaMonNavigation?.TenMon ?? "",
                    SoLuong = ct.SoLuong ?? 0,
                    DonGia = ct.DonGia ?? 0,
                    ThanhTien = (ct.SoLuong ?? 0) * (ct.DonGia ?? 0)
                })
                .ToList();

            string frxPath = Path.Combine(Application.StartupPath, "Reports", "HoaDon.frx");
            if (!File.Exists(frxPath))
            {
                frxPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "HoaDon.frx");
            }
            if (!File.Exists(frxPath))
            {
                frxPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Reports", "HoaDon.frx");
            }

            if (!File.Exists(frxPath))
            {
                error = $"Không tìm thấy file mẫu báo cáo tại: {frxPath}";
                return null;
            }

            Report report = new Report();
            report.Load(frxPath);
            report.RegisterData(listChiTiet, "ChiTiet");
            var ds = report.GetDataSource("ChiTiet");
            if (ds != null) ds.Enabled = true;

            report.SetParameterValue("TenQuan", "QUÁN CÀ PHÊ GÓC PHỐ");
            report.SetParameterValue("SoHD", $"HD{hd.MaHd:D6}");
            report.SetParameterValue("NgayLap", (hd.NgayLap ?? DateTime.Now).ToString("dd/MM/yyyy HH:mm"));
            report.SetParameterValue("SoBan", (hd.SoBan ?? 1).ToString());
            report.SetParameterValue("HinhThucTT", hd.HinhThucTt ?? "Tiền mặt");
            report.Prepare();

            return report;
        }

        private void BtnXemHoaDon_Click(object? sender, EventArgs e)
        {
            try
            {
                using var report = PrepareReport(out string error);
                if (report == null)
                {
                    MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                report.ShowPrepared();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXuatPdf_Click(object? sender, EventArgs e)
        {
            try
            {
                using var report = PrepareReport(out string error);
                if (report == null)
                {
                    MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var sfd = new SaveFileDialog();
                sfd.Filter = "Tài liệu PDF (*.pdf)|*.pdf";
                sfd.FileName = $"HoaDon_HD{_currentMaHd:D6}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var pdfExport = new PDFExport();
                    report.Export(pdfExport, sfd.FileName);

                    var res = MessageBox.Show("Xuất file PDF thành công! Bạn có muốn mở file ngay không?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (res == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIn_Click(object? sender, EventArgs e)
        {
            try
            {
                using var report = PrepareReport(out string error);
                if (report == null)
                {
                    MessageBox.Show(error, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                report.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }

    #region Helper View Models

    public class MonComboboxItem
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string DisplayText => $"{TenMon} ({DonGia.ToString("#,##0", Form1.VnCulture)} đ)";

        public override string ToString() => DisplayText;
    }

    public class HoaDonViewItem
    {
        public int MaHd { get; set; }
        public string MaHdHienThi => $"HD{MaHd:D6}";
        public DateTime? NgayLap { get; set; }
        public string NgayLapHienThi => NgayLap.HasValue ? NgayLap.Value.ToString("dd/MM/yyyy HH:mm") : "";
        public int? SoBan { get; set; }
        public string HinhThucTt { get; set; } = string.Empty;
        public string? GhiChu { get; set; }
        public decimal TongTien { get; set; }
        public string TongTienHienThi => TongTien.ToString("#,##0", Form1.VnCulture);
    }

    public class ChiTietViewItem
    {
        public int MaCt { get; set; }
        public int MaMon { get; set; }
        public int STT { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string DonGiaHienThi => DonGia.ToString("#,##0", Form1.VnCulture);
        public decimal ThanhTien => SoLuong * DonGia;
        public string ThanhTienHienThi => ThanhTien.ToString("#,##0", Form1.VnCulture);
    }

    #endregion
}
