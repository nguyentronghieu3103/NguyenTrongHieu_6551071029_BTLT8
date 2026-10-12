using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai1.Models;
using ClosedXML.Excel;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private int _selectedMaHang = 0;
        private bool _isBinding = false;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Nạp danh mục Loại hàng
                cboLoaiHang.Items.AddRange(new object[] {
                    "Đồ uống",
                    "Thực phẩm khô",
                    "Gia vị",
                    "Đồ gia dụng"
                });

                // Nạp danh mục Đơn vị
                cboDonVi.Items.AddRange(new object[] {
                    "Chai",
                    "Gói",
                    "Hộp",
                    "Túi"
                });

                // Nạp danh mục Lọc loại hàng (có mục "Tất cả loại")
                cboFilterLoai.Items.AddRange(new object[] {
                    "Tất cả loại",
                    "Đồ uống",
                    "Thực phẩm khô",
                    "Gia vị",
                    "Đồ gia dụng"
                });
                cboFilterLoai.SelectedIndex = 0;

                ResetInputs();
                await ApplyFilterAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo ứng dụng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Xử lý dữ liệu & Tìm kiếm

        private async Task ApplyFilterAsync()
        {
            try
            {
                using var db = new TiemTapHoaContext();
                var query = db.HangHoas.AsNoTracking().AsQueryable();

                // Lọc theo tên hàng
                string keyword = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(h => EF.Functions.Like(h.TenHang, $"%{keyword}%"));
                }

                // Lọc theo loại hàng
                string filterLoai = cboFilterLoai.SelectedItem?.ToString() ?? "Tất cả loại";
                if (filterLoai != "Tất cả" && filterLoai != "Tất cả loại")
                {
                    query = query.Where(h => h.LoaiHang == filterLoai);
                }

                // Lọc theo còn kinh doanh
                if (chkFilterConKinhDoanh.Checked)
                {
                    query = query.Where(h => h.ConKinhDoanh == true);
                }

                var list = await query.OrderBy(h => h.MaHang).ToListAsync();

                _isBinding = true;
                dgvHangHoa.DataSource = list;
                _isBinding = false;

                lblStatus.Text = $"Tìm thấy {list.Count} mặt hàng";

                if (list.Count > 0)
                {
                    var target = list.FirstOrDefault(x => x.MaHang == _selectedMaHang);
                    if (target != null)
                    {
                        int idx = list.IndexOf(target);
                        dgvHangHoa.Rows[idx].Selected = true;
                        DisplayHangHoa(target);
                    }
                    else
                    {
                        dgvHangHoa.Rows[0].Selected = true;
                        DisplayHangHoa(list[0]);
                    }
                }
                else
                {
                    ResetInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayHangHoa(HangHoa item)
        {
            if (item == null) return;
            _selectedMaHang = item.MaHang;
            txtTenHang.Text = item.TenHang ?? "";
            cboLoaiHang.Text = item.LoaiHang ?? "";
            cboDonVi.Text = item.DonVi ?? "";
            numGiaBan.Value = item.GiaBan.HasValue ? Math.Max(numGiaBan.Minimum, Math.Min(numGiaBan.Maximum, item.GiaBan.Value)) : 0;
            numSoLuongTon.Value = item.SoLuongTon.HasValue ? Math.Max(numSoLuongTon.Minimum, Math.Min(numSoLuongTon.Maximum, item.SoLuongTon.Value)) : 0;
            dtpNgayNhap.Value = item.NgayNhap ?? DateTime.Now;
            chkConKinhDoanh.Checked = item.ConKinhDoanh ?? false;
        }

        private void ResetInputs()
        {
            _selectedMaHang = 0;
            txtTenHang.Clear();
            cboLoaiHang.SelectedIndex = -1;
            cboDonVi.SelectedIndex = -1;
            numGiaBan.Value = 0;
            numSoLuongTon.Value = 0;
            dtpNgayNhap.Value = DateTime.Now;
            chkConKinhDoanh.Checked = true;
            dgvHangHoa.ClearSelection();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenHang.Text))
            {
                MessageBox.Show("Tên hàng không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenHang.Focus();
                return false;
            }

            if (numGiaBan.Value <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numGiaBan.Focus();
                return false;
            }

            return true;
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHangHoa.Rows.Count)
            {
                if (dgvHangHoa.Rows[e.RowIndex].DataBoundItem is HangHoa item)
                {
                    DisplayHangHoa(item);
                }
            }
        }

        private void dgvHangHoa_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isBinding && dgvHangHoa.CurrentRow != null && dgvHangHoa.CurrentRow.Index >= 0)
            {
                if (dgvHangHoa.CurrentRow.DataBoundItem is HangHoa item)
                {
                    DisplayHangHoa(item);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            timerSearch.Stop();
            timerSearch.Start();
        }

        private async void timerSearch_Tick(object sender, EventArgs e)
        {
            timerSearch.Stop();
            await ApplyFilterAsync();
        }

        private async void cboFilterLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                await ApplyFilterAsync();
            }
        }

        private async void chkFilterConKinhDoanh_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                await ApplyFilterAsync();
            }
        }

        #endregion

        #region Thêm, Sửa, Xóa, Làm mới

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                using var db = new TiemTapHoaContext();
                var hh = new HangHoa
                {
                    TenHang = txtTenHang.Text.Trim(),
                    LoaiHang = string.IsNullOrWhiteSpace(cboLoaiHang.Text) ? null : cboLoaiHang.Text.Trim(),
                    DonVi = string.IsNullOrWhiteSpace(cboDonVi.Text) ? null : cboDonVi.Text.Trim(),
                    GiaBan = numGiaBan.Value,
                    SoLuongTon = (int)numSoLuongTon.Value,
                    NgayNhap = dtpNgayNhap.Value,
                    ConKinhDoanh = chkConKinhDoanh.Checked
                };

                await db.HangHoas.AddAsync(hh);
                await db.SaveChangesAsync();

                _selectedMaHang = hh.MaHang;
                MessageBox.Show("Thêm hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ApplyFilterAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedMaHang <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng hóa cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput()) return;

                using var db = new TiemTapHoaContext();
                var hh = await db.HangHoas.FindAsync(_selectedMaHang);
                if (hh == null)
                {
                    MessageBox.Show("Không tìm thấy hàng hóa cần sửa trong CSDL!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                hh.TenHang = txtTenHang.Text.Trim();
                hh.LoaiHang = string.IsNullOrWhiteSpace(cboLoaiHang.Text) ? null : cboLoaiHang.Text.Trim();
                hh.DonVi = string.IsNullOrWhiteSpace(cboDonVi.Text) ? null : cboDonVi.Text.Trim();
                hh.GiaBan = numGiaBan.Value;
                hh.SoLuongTon = (int)numSoLuongTon.Value;
                hh.NgayNhap = dtpNgayNhap.Value;
                hh.ConKinhDoanh = chkConKinhDoanh.Checked;

                await db.SaveChangesAsync();
                MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ApplyFilterAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedMaHang <= 0)
                {
                    MessageBox.Show("Vui lòng chọn hàng hóa cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa hàng hóa '{txtTenHang.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;

                using var db = new TiemTapHoaContext();
                var hh = await db.HangHoas.FindAsync(_selectedMaHang);
                if (hh != null)
                {
                    db.HangHoas.Remove(hh);
                    await db.SaveChangesAsync();
                    _selectedMaHang = 0;
                    MessageBox.Show("Xóa hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetInputs();
                    await ApplyFilterAsync();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hàng hóa cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterLoai.SelectedIndex = 0;
            chkFilterConKinhDoanh.Checked = false;
            ResetInputs();
            await ApplyFilterAsync();
        }

        #endregion

        #region FastReport Bảng Giá & PDF

        private async Task<Report> PrepareBangGiaReportAsync()
        {
            using var db = new TiemTapHoaContext();
            var list = await db.HangHoas.AsNoTracking()
                .Where(h => h.ConKinhDoanh == true)
                .OrderBy(h => h.MaHang)
                .ToListAsync();

            var dt = new DataTable("HangHoa");
            dt.Columns.Add("MaHang", typeof(int));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("LoaiHang", typeof(string));
            dt.Columns.Add("DonVi", typeof(string));
            dt.Columns.Add("GiaBan", typeof(decimal));
            dt.Columns.Add("SoLuongTon", typeof(int));
            dt.Columns.Add("NgayNhap", typeof(DateTime));
            dt.Columns.Add("ConKinhDoanh", typeof(bool));

            foreach (var item in list)
            {
                dt.Rows.Add(
                    item.MaHang,
                    item.TenHang,
                    item.LoaiHang,
                    item.DonVi,
                    item.GiaBan ?? 0m,
                    item.SoLuongTon ?? 0,
                    item.NgayNhap ?? DateTime.Now,
                    item.ConKinhDoanh ?? false
                );
            }

            Report report = new Report();

            // Tìm đường dẫn file .frx trong thư mục Reports
            string frxPath = Path.Combine(Application.StartupPath, "Reports", "BangGia.frx");
            if (!File.Exists(frxPath))
            {
                frxPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BangGia.frx");
            }
            if (!File.Exists(frxPath))
            {
                frxPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Reports", "BangGia.frx");
            }

            if (!File.Exists(frxPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file mẫu báo cáo tại: {frxPath}");
            }

            report.Load(frxPath);
            report.RegisterData(dt, "HangHoa");
            report.GetDataSource("HangHoa").Enabled = true;
            report.SetParameterValue("NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
            report.Prepare();

            return report;
        }

        private async void btnXemBangGia_Click(object sender, EventArgs e)
        {
            try
            {
                using Report report = await PrepareBangGiaReportAsync();
                report.ShowPrepared();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem bảng giá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXuatPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = "BangGia.pdf",
                    Title = "Xuất Bảng giá ra PDF"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                using Report report = await PrepareBangGiaReportAsync();
                PDFExport pdfExport = new PDFExport();
                report.Export(pdfExport, sfd.FileName);

                var dr = MessageBox.Show("Xuất file PDF thành công! Bạn có muốn mở file không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (IOException)
            {
                MessageBox.Show("File PDF đang được mở bởi ứng dụng khác. Vui lòng đóng file và thử lại!", "Lỗi xuất PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Xuất Excel (ClosedXML)

        private void btnXuatExcelLuoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHangHoa.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trên lưới để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "DanhSachHangHoa.xlsx",
                    Title = "Xuất danh sách hàng hóa từ lưới"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("HangHoa");

                // Lấy các cột hiển thị trên DataGridView
                var visibleColumns = dgvHangHoa.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.Visible)
                    .OrderBy(c => c.DisplayIndex)
                    .ToList();

                // Tạo dòng tiêu đề
                for (int col = 0; col < visibleColumns.Count; col++)
                {
                    var cell = worksheet.Cell(1, col + 1);
                    cell.Value = visibleColumns[col].HeaderText;
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(220, 230, 242);
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Đổ dữ liệu từ lưới, số là kiểu số (numeric) thực tế
                int rowIndex = 2;
                foreach (DataGridViewRow dgvRow in dgvHangHoa.Rows)
                {
                    if (dgvRow.IsNewRow) continue;

                    for (int col = 0; col < visibleColumns.Count; col++)
                    {
                        var cell = worksheet.Cell(rowIndex, col + 1);
                        var colObj = visibleColumns[col];
                        object? val = dgvRow.Cells[colObj.Name].Value;

                        if (val == null || val == DBNull.Value)
                        {
                            cell.Value = Blank.Value;
                        }
                        else if (val is int intVal)
                        {
                            cell.Value = intVal;
                            cell.Style.NumberFormat.Format = "#,##0";
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        }
                        else if (val is decimal decVal)
                        {
                            cell.Value = decVal;
                            cell.Style.NumberFormat.Format = "#,##0";
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        }
                        else if (val is double dblVal)
                        {
                            cell.Value = dblVal;
                            cell.Style.NumberFormat.Format = "#,##0";
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        }
                        else if (val is DateTime dtVal)
                        {
                            cell.Value = dtVal;
                            cell.Style.DateFormat.Format = "dd/MM/yyyy";
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }
                        else if (val is bool bVal)
                        {
                            cell.Value = bVal ? "Còn bán" : "Ngừng bán";
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }
                        else
                        {
                            cell.Value = val.ToString();
                        }

                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    rowIndex++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(sfd.FileName);

                var dr = MessageBox.Show("Xuất file Excel thành công! Bạn có muốn mở file không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (IOException)
            {
                MessageBox.Show("File Excel đang được mở bởi ứng dụng khác. Vui lòng đóng file và thử lại!", "Lỗi xuất Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXuatExcelTonKho_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "BaoCaoTonKho.xlsx",
                    Title = "Xuất báo cáo tồn kho"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                // Truy vấn lại bằng EF Core các hàng còn kinh doanh
                using var db = new TiemTapHoaContext();
                var list = await db.HangHoas.AsNoTracking()
                    .Where(h => h.ConKinhDoanh == true)
                    .OrderBy(h => h.MaHang)
                    .ToListAsync();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("TonKho");

                // Tiêu đề cột
                string[] headers = new string[] {
                    "STT",
                    "Mã hàng",
                    "Tên hàng",
                    "Loại hàng",
                    "Đơn vị",
                    "Giá bán",
                    "Số lượng tồn",
                    "Giá trị tồn"
                };

                for (int col = 0; col < headers.Length; col++)
                {
                    var cell = worksheet.Cell(1, col + 1);
                    cell.Value = headers[col];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(220, 230, 242);
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                int row = 2;
                for (int i = 0; i < list.Count; i++)
                {
                    var hh = list[i];

                    // STT
                    var cellStt = worksheet.Cell(row, 1);
                    cellStt.Value = i + 1;
                    cellStt.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cellStt.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Mã hàng
                    var cellMa = worksheet.Cell(row, 2);
                    cellMa.Value = hh.MaHang;
                    cellMa.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cellMa.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Tên hàng
                    var cellTen = worksheet.Cell(row, 3);
                    cellTen.Value = hh.TenHang ?? "";
                    cellTen.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Loại hàng
                    var cellLoai = worksheet.Cell(row, 4);
                    cellLoai.Value = hh.LoaiHang ?? "";
                    cellLoai.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Đơn vị
                    var cellDonVi = worksheet.Cell(row, 5);
                    cellDonVi.Value = hh.DonVi ?? "";
                    cellDonVi.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cellDonVi.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Giá bán (F) - số căn phải
                    var cellGia = worksheet.Cell(row, 6);
                    cellGia.Value = hh.GiaBan ?? 0m;
                    cellGia.Style.NumberFormat.Format = "#,##0";
                    cellGia.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    cellGia.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Số lượng tồn (G) - số căn phải
                    var cellTon = worksheet.Cell(row, 7);
                    cellTon.Value = hh.SoLuongTon ?? 0;
                    cellTon.Style.NumberFormat.Format = "#,##0";
                    cellTon.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    cellTon.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Giá trị tồn (H) - công thức = F*G
                    var cellGiaTri = worksheet.Cell(row, 8);
                    cellGiaTri.FormulaA1 = $"=F{row}*G{row}";
                    cellGiaTri.Style.NumberFormat.Format = "#,##0";
                    cellGiaTri.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    cellGiaTri.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    row++;
                }

                // Dòng Tổng dùng công thức SUM
                int totalRow = row;
                var cellTongLabel = worksheet.Cell(totalRow, 7);
                cellTongLabel.Value = "Tổng";
                cellTongLabel.Style.Font.Bold = true;
                cellTongLabel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                cellTongLabel.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Viền cho các ô từ cột 1 đến 6 của dòng tổng
                for (int c = 1; c <= 6; c++)
                {
                    worksheet.Cell(totalRow, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                var cellTongValue = worksheet.Cell(totalRow, 8);
                cellTongValue.FormulaA1 = $"=SUM(H2:H{totalRow - 1})";
                cellTongValue.Style.Font.Bold = true;
                cellTongValue.Style.NumberFormat.Format = "#,##0";
                cellTongValue.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                cellTongValue.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                cellTongValue.Style.Border.BottomBorder = XLBorderStyleValues.Double;

                // AutoFilter trên bảng dữ liệu (không bao gồm dòng tổng)
                worksheet.Range(1, 1, totalRow - 1, headers.Length).SetAutoFilter();

                // Cố định dòng tiêu đề (Freeze Header)
                worksheet.SheetView.FreezeRows(1);

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(sfd.FileName);

                var dr = MessageBox.Show("Xuất báo cáo tồn kho thành công! Bạn có muốn mở file không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (IOException)
            {
                MessageBox.Show("File Excel đang được mở bởi ứng dụng khác. Vui lòng đóng file và thử lại!", "Lỗi xuất Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
