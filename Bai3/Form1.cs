using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Bai3.Models;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace Bai3
{
    public partial class Form1 : Form
    {
        private QuanLyDiemContext _context = null!;
        private readonly CultureInfo _viCulture = new CultureInfo("vi-VN");
        private bool _isLoading = false;

        private SinhVien? _currentSinhVien;
        private List<DiemMon> _currentDiemList = new();
        private int? _selectedDiemMaDiem;
        private decimal _currentDtb = 0;
        private string _currentXepLoai = "";

        private readonly PhieuDiemPrinter _phieuPrinter = new();

        private class LopItem
        {
            public string MaLop { get; set; } = "";
            public string DisplayText { get; set; } = "";
            public override string ToString() => DisplayText;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;
                _context = new QuanLyDiemContext();

                LoadLopData();
                LoadSinhVienList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động cơ sở dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
                // Kích hoạt hiển thị cho sinh viên đầu tiên nếu có
                if (dgvSinhVien.Rows.Count > 0)
                {
                    dgvSinhVien_SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        private void LoadLopData()
        {
            var lops = _context.Lops.OrderBy(l => l.MaLop).ToList();

            // Load vào ListBox bên trái
            lstLop.Items.Clear();
            lstLop.Items.Add(new LopItem { MaLop = "", DisplayText = "Tất cả" });
            foreach (var l in lops)
            {
                lstLop.Items.Add(new LopItem { MaLop = l.MaLop, DisplayText = $"{l.MaLop} - {l.TenLop}" });
            }
            lstLop.SelectedIndex = 0;

            // Load vào ComboBox trong Thông tin sinh viên
            cboLop.DataSource = null;
            cboLop.DisplayMember = "MaLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = lops;
        }

        private void LoadSinhVienList(string? selectMaSv = null)
        {
            string? selectedMaLop = null;
            if (lstLop.SelectedItem is LopItem lopItem && !string.IsNullOrEmpty(lopItem.MaLop))
            {
                selectedMaLop = lopItem.MaLop;
            }

            string keyword = txtTimKiem.Text.Trim().ToLower();

            var query = _context.SinhViens
                .Include(s => s.DiemMons)
                .AsQueryable();

            if (!string.IsNullOrEmpty(selectedMaLop))
            {
                query = query.Where(s => s.MaLop == selectedMaLop);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s => s.MaSv.ToLower().Contains(keyword) || (s.HoTen != null && s.HoTen.ToLower().Contains(keyword)));
            }

            var list = query.OrderBy(s => s.MaSv).ToList();

            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.DataSource = list;

            if (list.Count > 0)
            {
                int selectIdx = 0;
                if (!string.IsNullOrEmpty(selectMaSv))
                {
                    int found = list.FindIndex(s => s.MaSv == selectMaSv);
                    if (found >= 0) selectIdx = found;
                }
                dgvSinhVien.ClearSelection();
                dgvSinhVien.Rows[selectIdx].Selected = true;
                dgvSinhVien.CurrentCell = dgvSinhVien.Rows[selectIdx].Cells[0];
            }
            else
            {
                ClearSinhVienDetails();
            }
        }

        private void ClearSinhVienDetails()
        {
            _currentSinhVien = null;
            _currentDiemList.Clear();
            _selectedDiemMaDiem = null;

            txtMaSV.Text = "";
            txtHoTen.Text = "";
            dtpNgaySinh.Value = DateTime.Today;
            radNam.Checked = true;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;

            dgvDiemMon.DataSource = null;
            txtTenMon.Text = "";
            nudSoTC.Value = 3;
            nudDiem.Value = 0;

            lblTongTC.Text = "Tổng tín chỉ: 0";
            lblDiemTB.Text = "Điểm TB: Chưa có điểm";
            lblXepLoai.Text = "Xếp loại: Chưa có điểm";
        }

        private void dgvSinhVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;

            if (dgvSinhVien.CurrentRow != null && dgvSinhVien.CurrentRow.DataBoundItem is SinhVien sv)
            {
                _currentSinhVien = _context.SinhViens
                    .Include(s => s.DiemMons)
                    .Include(s => s.MaLopNavigation)
                    .FirstOrDefault(s => s.MaSv == sv.MaSv);

                if (_currentSinhVien != null)
                {
                    DisplaySinhVienDetails(_currentSinhVien);
                }
            }
            else
            {
                ClearSinhVienDetails();
            }
        }

        private void DisplaySinhVienDetails(SinhVien sv)
        {
            txtMaSV.Text = sv.MaSv;
            txtHoTen.Text = sv.HoTen ?? "";
            dtpNgaySinh.Value = sv.NgaySinh ?? DateTime.Today;

            if (string.Equals(sv.GioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase))
            {
                radNu.Checked = true;
            }
            else
            {
                radNam.Checked = true;
            }

            if (!string.IsNullOrEmpty(sv.MaLop))
            {
                cboLop.SelectedValue = sv.MaLop;
            }

            LoadDiemList(sv);
        }

        private void LoadDiemList(SinhVien sv, int? selectMaDiem = null)
        {
            _currentDiemList = sv.DiemMons.OrderBy(d => d.MaDiem).ToList();

            var displayList = _currentDiemList.Select((d, idx) => new
            {
                STT = idx + 1,
                MaDiem = d.MaDiem,
                TenMon = d.TenMon,
                SoTinChi = d.SoTinChi,
                Diem = d.Diem,
                DiemDisplay = d.Diem.HasValue ? d.Diem.Value.ToString("0.0", _viCulture) : ""
            }).ToList();

            dgvDiemMon.AutoGenerateColumns = false;
            dgvDiemMon.DataSource = displayList;

            // Tính điểm trung bình và xếp loại
            if (_currentDiemList.Count == 0)
            {
                _currentDtb = 0;
                _currentXepLoai = "Chưa có điểm";
                lblTongTC.Text = "Tổng tín chỉ: 0";
                lblDiemTB.Text = "Điểm TB: Chưa có điểm";
                lblXepLoai.Text = "Xếp loại: Chưa có điểm";
            }
            else
            {
                int tongTC = _currentDiemList.Sum(d => d.SoTinChi ?? 0);
                decimal tongDiemNhanTC = _currentDiemList.Sum(d => (d.Diem ?? 0) * (d.SoTinChi ?? 0));
                _currentDtb = tongTC > 0 ? Math.Round(tongDiemNhanTC / tongTC, 2, MidpointRounding.AwayFromZero) : 0;

                if (_currentDtb >= 8.0m) _currentXepLoai = "Giỏi";
                else if (_currentDtb >= 7.0m) _currentXepLoai = "Khá";
                else if (_currentDtb >= 5.0m) _currentXepLoai = "Trung bình";
                else _currentXepLoai = "Yếu";

                lblTongTC.Text = $"Tổng tín chỉ: {tongTC}";
                lblDiemTB.Text = $"Điểm TB: {_currentDtb.ToString("0.00", _viCulture)}";
                lblXepLoai.Text = $"Xếp loại: {_currentXepLoai}";
            }

            // Chọn dòng môn học nếu có
            if (displayList.Count > 0)
            {
                int selectIdx = 0;
                if (selectMaDiem.HasValue)
                {
                    int found = displayList.FindIndex(d => d.MaDiem == selectMaDiem.Value);
                    if (found >= 0) selectIdx = found;
                }
                dgvDiemMon.ClearSelection();
                dgvDiemMon.Rows[selectIdx].Selected = true;
                dgvDiemMon.CurrentCell = dgvDiemMon.Rows[selectIdx].Cells[0];
            }
            else
            {
                txtTenMon.Text = "";
                nudSoTC.Value = 3;
                nudDiem.Value = 0;
                _selectedDiemMaDiem = null;
            }
        }

        private void dgvDiemMon_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvDiemMon.CurrentRow != null && dgvDiemMon.CurrentRow.DataBoundItem != null)
            {
                dynamic item = dgvDiemMon.CurrentRow.DataBoundItem;
                _selectedDiemMaDiem = (int)item.MaDiem;
                txtTenMon.Text = item.TenMon ?? "";

                int soTc = item.SoTinChi ?? 3;
                nudSoTC.Value = Math.Clamp(soTc, 1, 6);

                decimal diemVal = item.Diem ?? 0;
                nudDiem.Value = Math.Clamp(diemVal, 0, 10);
            }
            else
            {
                _selectedDiemMaDiem = null;
            }
        }

        private void lstLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            LoadSinhVienList();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            LoadSinhVienList();
        }

        #region Thêm - Sửa - Xóa Sinh viên

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            string maSv = txtMaSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();

            if (string.IsNullOrEmpty(maSv))
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên sinh viên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (_context.SinhViens.Any(s => s.MaSv == maSv))
            {
                MessageBox.Show($"Mã sinh viên '{maSv}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.", "Trùng mã sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return;
            }

            string maLop = cboLop.SelectedValue?.ToString() ?? cboLop.Text;
            if (string.IsNullOrEmpty(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp cho sinh viên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sv = new SinhVien
                {
                    MaSv = maSv,
                    HoTen = hoTen,
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = radNu.Checked ? "Nữ" : "Nam",
                    MaLop = maLop
                };

                _context.SinhViens.Add(sv);
                _context.SaveChanges();

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSinhVienList(maSv);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            if (_currentSinhVien == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên sinh viên không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            string maLop = cboLop.SelectedValue?.ToString() ?? cboLop.Text;

            try
            {
                var svInDb = _context.SinhViens.Find(_currentSinhVien.MaSv);
                if (svInDb != null)
                {
                    svInDb.HoTen = hoTen;
                    svInDb.NgaySinh = dtpNgaySinh.Value.Date;
                    svInDb.GioiTinh = radNu.Checked ? "Nữ" : "Nam";
                    svInDb.MaLop = maLop;

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSinhVienList(_currentSinhVien.MaSv);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            if (_currentSinhVien == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dialogResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên '{_currentSinhVien.HoTen}' ({_currentSinhVien.MaSv})?\nToàn bộ điểm thi của sinh viên này cũng sẽ bị xóa!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    // Xóa các dòng điểm của sinh viên đó trước
                    var diems = _context.DiemMons.Where(d => d.MaSv == _currentSinhVien.MaSv).ToList();
                    _context.DiemMons.RemoveRange(diems);

                    // Xóa sinh viên
                    var svInDb = _context.SinhViens.Find(_currentSinhVien.MaSv);
                    if (svInDb != null)
                    {
                        _context.SinhViens.Remove(svInDb);
                    }

                    _context.SaveChanges();
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSinhVienList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Thêm - Sửa - Xóa Điểm Môn

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (_currentSinhVien == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên trước khi thêm môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên môn học không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            try
            {
                var dm = new DiemMon
                {
                    MaSv = _currentSinhVien.MaSv,
                    TenMon = tenMon,
                    SoTinChi = (int)nudSoTC.Value,
                    Diem = nudDiem.Value
                };

                _context.DiemMons.Add(dm);
                _context.SaveChanges();

                // Nạp lại sinh viên để làm mới danh sách môn học và cập nhật Điểm TB, Xếp loại ngay
                _context.Entry(_currentSinhVien).Collection(s => s.DiemMons).Load();
                LoadDiemList(_currentSinhVien, dm.MaDiem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaDiem_Click(object sender, EventArgs e)
        {
            if (_currentSinhVien == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_selectedDiemMaDiem.HasValue)
            {
                MessageBox.Show("Vui lòng chọn môn học cần sửa trong bảng điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên môn học không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            try
            {
                var dm = _context.DiemMons.Find(_selectedDiemMaDiem.Value);
                if (dm != null)
                {
                    dm.TenMon = tenMon;
                    dm.SoTinChi = (int)nudSoTC.Value;
                    dm.Diem = nudDiem.Value;

                    _context.SaveChanges();

                    _context.Entry(_currentSinhVien).Collection(s => s.DiemMons).Load();
                    LoadDiemList(_currentSinhVien, dm.MaDiem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa điểm môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (_currentSinhVien == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_selectedDiemMaDiem.HasValue)
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa trong bảng điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa môn '{txtTenMon.Text}' của sinh viên này?", "Xác nhận xóa môn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    var dm = _context.DiemMons.Find(_selectedDiemMaDiem.Value);
                    if (dm != null)
                    {
                        _context.DiemMons.Remove(dm);
                        _context.SaveChanges();

                        _context.Entry(_currentSinhVien).Collection(s => s.DiemMons).Load();
                        LoadDiemList(_currentSinhVien);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region In ấn & Xem trước phiếu báo điểm

        private bool ValidateAndPreparePrinter()
        {
            if (_currentSinhVien == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để in phiếu điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_currentDiemList == null || _currentDiemList.Count == 0)
            {
                MessageBox.Show("Sinh viên chưa có điểm môn nào, không thể in phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string tenKhoa = "KHOA CÔNG NGHỆ THÔNG TIN";
            if (!string.IsNullOrEmpty(_currentSinhVien.MaLop))
            {
                var lop = _context.Lops.Find(_currentSinhVien.MaLop);
                if (lop != null && lop.TenLop != null)
                {
                    if (lop.TenLop.Contains("Kế toán", StringComparison.OrdinalIgnoreCase))
                    {
                        tenKhoa = "KHOA KẾ TOÁN - TÀI CHÍNH";
                    }
                }
            }

            _phieuPrinter.SinhVien = _currentSinhVien;
            _phieuPrinter.DiemMons = _currentDiemList;
            _phieuPrinter.DiemTB = _currentDtb;
            _phieuPrinter.XepLoai = _currentXepLoai;
            _phieuPrinter.TenKhoa = tenKhoa;

            return true;
        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            if (!ValidateAndPreparePrinter()) return;

            try
            {
                using var preview = new PrintPreviewDialog();
                preview.Document = _phieuPrinter.PrintDocument;
                preview.WindowState = FormWindowState.Maximized;
                preview.ShowDialog(this);
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show("Hệ thống chưa cài đặt máy in hoặc dịch vụ in không khả dụng.\nChi tiết: " + ex.Message, "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị bản xem trước: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (!ValidateAndPreparePrinter()) return;

            try
            {
                using var printDlg = new PrintDialog();
                printDlg.Document = _phieuPrinter.PrintDocument;
                printDlg.UseEXDialog = true;

                if (printDlg.ShowDialog(this) == DialogResult.OK)
                {
                    _phieuPrinter.PrintDocument.Print();
                }
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show("Hệ thống chưa cài đặt máy in hoặc dịch vụ in không khả dụng.\nChi tiết: " + ex.Message, "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCaiDatTrang_Click(object sender, EventArgs e)
        {
            if (!ValidateAndPreparePrinter()) return;

            try
            {
                using var pageSetup = new PageSetupDialog();
                pageSetup.Document = _phieuPrinter.PrintDocument;
                pageSetup.EnableMetric = true;
                pageSetup.ShowDialog(this);
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show("Hệ thống chưa cài đặt máy in hoặc dịch vụ in không khả dụng.\nChi tiết: " + ex.Message, "Lỗi máy in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cài đặt trang: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Xuất Excel (ClosedXML)

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            string? maLop = null;

            if (lstLop.SelectedItem is LopItem lopItem && !string.IsNullOrEmpty(lopItem.MaLop))
            {
                maLop = lopItem.MaLop;
            }
            else if (_currentSinhVien != null && !string.IsNullOrEmpty(_currentSinhVien.MaLop))
            {
                var ask = MessageBox.Show($"Bạn đang chọn 'Tất cả' lớp. Bạn có muốn xuất bảng điểm cho lớp '{_currentSinhVien.MaLop}' của sinh viên đang chọn không?", "Xác nhận xuất lớp", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask == DialogResult.Yes)
                {
                    maLop = _currentSinhVien.MaLop;
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp cụ thể trong danh sách 'Lớp' bên trái để xuất điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Truy vấn nối 3 bảng bằng EF Core cho lớp đang chọn:
                // MSSV, Họ tên, Lớp, Môn học, Số TC, Điểm
                var exportData = (from sv in _context.SinhViens
                                  join lp in _context.Lops on sv.MaLop equals lp.MaLop
                                  join dm in _context.DiemMons on sv.MaSv equals dm.MaSv
                                  where lp.MaLop == maLop
                                  orderby sv.MaSv, dm.MaDiem
                                  select new
                                  {
                                      sv.MaSv,
                                      sv.HoTen,
                                      lp.MaLop,
                                      dm.TenMon,
                                      dm.SoTinChi,
                                      dm.Diem
                                  }).ToList();

                if (exportData.Count == 0)
                {
                    MessageBox.Show($"Lớp '{maLop}' chưa có dữ liệu điểm nào để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"BangDiem_{maLop}.xlsx";
                saveDialog.Title = $"Xuất bảng điểm lớp {maLop}";

                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    using var workbook = new XLWorkbook();
                    var ws = workbook.Worksheets.Add($"Điểm_{maLop}");

                    // Tiêu đề bảng
                    ws.Cell("A1").Value = $"BẢNG ĐIỂM LỚP {maLop}";
                    ws.Range("A1:F1").Merge().Style
                        .Font.SetBold(true)
                        .Font.SetFontSize(16)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    // Header
                    int headerRow = 3;
                    string[] headers = { "MSSV", "Họ tên", "Lớp", "Môn học", "Số TC", "Điểm" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = ws.Cell(headerRow, i + 1);
                        cell.Value = headers[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#D9E1F2");
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    // Dòng dữ liệu
                    int dataStartRow = headerRow + 1;
                    int currentRow = dataStartRow;

                    foreach (var item in exportData)
                    {
                        ws.Cell(currentRow, 1).SetValue(item.MaSv).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(currentRow, 2).SetValue(item.HoTen).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRow, 3).SetValue(item.MaLop).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(currentRow, 4).SetValue(item.TenMon).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        ws.Cell(currentRow, 5).SetValue(item.SoTinChi ?? 0).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        if (item.Diem.HasValue)
                        {
                            ws.Cell(currentRow, 6).SetValue((double)item.Diem.Value).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                            ws.Cell(currentRow, 6).Style.NumberFormat.Format = "0.00";
                        }
                        else
                        {
                            ws.Cell(currentRow, 6).SetValue(0).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        }

                        for (int col = 1; col <= 6; col++)
                        {
                            ws.Cell(currentRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        currentRow++;
                    }

                    int dataEndRow = currentRow - 1;

                    // Cuối bảng thêm hai dòng công thức:
                    // 1. “Tổng số tín chỉ” dùng SUM cột Số TC
                    ws.Range(currentRow, 1, currentRow, 4).Merge();
                    ws.Cell(currentRow, 1).Value = "Tổng số tín chỉ";
                    ws.Cell(currentRow, 1).Style.Font.Bold = true;
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                    var cellSumTC = ws.Cell(currentRow, 5);
                    cellSumTC.FormulaA1 = $"SUM(E{dataStartRow}:E{dataEndRow})";
                    cellSumTC.Style.Font.Bold = true;
                    cellSumTC.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    for (int col = 1; col <= 6; col++)
                    {
                        ws.Cell(currentRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    currentRow++;

                    // 2. “Điểm trung bình” dùng AVERAGE cột Điểm
                    ws.Range(currentRow, 1, currentRow, 5).Merge();
                    ws.Cell(currentRow, 1).Value = "Điểm trung bình";
                    ws.Cell(currentRow, 1).Style.Font.Bold = true;
                    ws.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                    var cellAvgDiem = ws.Cell(currentRow, 6);
                    cellAvgDiem.FormulaA1 = $"AVERAGE(F{dataStartRow}:F{dataEndRow})";
                    cellAvgDiem.Style.Font.Bold = true;
                    cellAvgDiem.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    cellAvgDiem.Style.NumberFormat.Format = "0.00";

                    for (int col = 1; col <= 6; col++)
                    {
                        ws.Cell(currentRow, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    ws.Columns().AdjustToContents();

                    workbook.SaveAs(saveDialog.FileName);

                    var openFile = MessageBox.Show($"Xuất Excel thành công vào file:\n{saveDialog.FileName}\n\nBạn có muốn mở file này ngay bây giờ không?", "Thành công", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (openFile == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
