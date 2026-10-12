using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using Bai3.Models;

namespace Bai3
{
    public class PhieuDiemPrinter
    {
        public PrintDocument PrintDocument { get; }
        public SinhVien? SinhVien { get; set; }
        public List<DiemMon> DiemMons { get; set; } = new();
        public decimal DiemTB { get; set; }
        public string XepLoai { get; set; } = "";
        public string TenKhoa { get; set; } = "KHOA CÔNG NGHỆ THÔNG TIN";

        private int _currentPage;
        private int _totalPages;
        private int _currentItemIndex;
        private readonly CultureInfo _viCulture = new CultureInfo("vi-VN");

        public PhieuDiemPrinter()
        {
            PrintDocument = new PrintDocument();
            PrintDocument.DocumentName = "Phiếu báo điểm";
            PrintDocument.BeginPrint += PrintDocument_BeginPrint;
            PrintDocument.PrintPage += PrintDocument_PrintPage;
        }

        public PhieuDiemPrinter(SinhVien sv, List<DiemMon> diems, decimal dtb, string xepLoai, string tenKhoa = "KHOA CÔNG NGHỆ THÔNG TIN")
            : this()
        {
            SinhVien = sv;
            DiemMons = diems;
            DiemTB = dtb;
            XepLoai = xepLoai;
            TenKhoa = tenKhoa;
        }

        private void PrintDocument_BeginPrint(object? sender, PrintEventArgs e)
        {
            _currentPage = 0;
            _currentItemIndex = 0;

            // Tính toán tổng số trang dựa trên margin bounds hiện tại
            var margins = PrintDocument.DefaultPageSettings.Margins;
            var bounds = PrintDocument.DefaultPageSettings.Bounds;

            int pageHeight = bounds.Height;
            if (PrintDocument.DefaultPageSettings.Landscape && bounds.Width < bounds.Height)
            {
                pageHeight = bounds.Width;
            }

            int marginHeight = pageHeight - margins.Top - margins.Bottom;
            if (marginHeight <= 0) marginHeight = 800; // Fallback an toàn

            _totalPages = CalculateTotalPages(marginHeight, DiemMons.Count);
        }

        private int CalculateTotalPages(int marginHeight, int totalItems)
        {
            if (totalItems == 0) return 1;

            int rowHeight = 25;
            int tableHeaderHeight = 26;
            int footerHeight = 35;
            int summaryAndSigHeight = 135;
            int p1HeaderHeight = 150;

            // Nếu chỉ có 1 trang và chứa được cả danh sách điểm + phần tổng kết & chữ ký
            int p1AvailableWithSig = marginHeight - p1HeaderHeight - tableHeaderHeight - footerHeight - summaryAndSigHeight;
            int maxP1WithSig = Math.Max(1, p1AvailableWithSig / rowHeight);

            if (totalItems <= maxP1WithSig)
            {
                return 1;
            }

            // Nếu cần nhiều hơn 1 trang:
            // Trang 1 không có chữ ký
            int p1Available = marginHeight - p1HeaderHeight - tableHeaderHeight - footerHeight;
            int maxP1 = Math.Max(1, p1Available / rowHeight);

            int remaining = totalItems - maxP1;
            int pages = 1;

            int contAvailable = marginHeight - tableHeaderHeight - footerHeight;
            int maxContNoSig = Math.Max(1, contAvailable / rowHeight);
            int maxContWithSig = Math.Max(1, (contAvailable - summaryAndSigHeight) / rowHeight);

            while (remaining > 0)
            {
                pages++;
                if (remaining <= maxContWithSig)
                {
                    remaining = 0;
                    break;
                }
                else
                {
                    remaining -= maxContNoSig;
                }
            }

            return pages;
        }

        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (SinhVien == null || e.Graphics == null)
            {
                e.HasMorePages = false;
                return;
            }

            _currentPage++;

            float left = e.MarginBounds.Left;
            float top = e.MarginBounds.Top;
            float right = e.MarginBounds.Right;
            float bottom = e.MarginBounds.Bottom;
            float width = e.MarginBounds.Width;

            float currentY = top;

            // 1. In phần đầu trang (Header)
            if (_currentPage == 1)
            {
                // Tên khoa
                using (var fontFaculty = new Font("Arial", 10.5f, FontStyle.Regular))
                {
                    e.Graphics.DrawString(TenKhoa, fontFaculty, Brushes.Black, left, currentY);
                }
                currentY += 28;

                // Tiêu đề: PHIẾU BÁO ĐIỂM
                using (var fontTitle = new Font("Arial", 16, FontStyle.Bold))
                {
                    string title = "PHIẾU BÁO ĐIỂM";
                    var titleSize = e.Graphics.MeasureString(title, fontTitle);
                    float titleX = left + (width - titleSize.Width) / 2;
                    e.Graphics.DrawString(title, fontTitle, Brushes.Black, titleX, currentY);
                }
                currentY += 40;

                // Thông tin sinh viên
                using (var fontInfo = new Font("Arial", 10, FontStyle.Regular))
                {
                    float lineHeight = 20;
                    e.Graphics.DrawString($"MSSV: {SinhVien.MaSv}", fontInfo, Brushes.Black, left, currentY);
                    currentY += lineHeight;
                    e.Graphics.DrawString($"Họ tên: {SinhVien.HoTen}", fontInfo, Brushes.Black, left, currentY);
                    currentY += lineHeight;
                    string ngaySinhStr = SinhVien.NgaySinh.HasValue ? SinhVien.NgaySinh.Value.ToString("dd/MM/yyyy") : "";
                    e.Graphics.DrawString($"Ngày sinh: {ngaySinhStr}", fontInfo, Brushes.Black, left, currentY);
                    currentY += lineHeight;
                    e.Graphics.DrawString($"Lớp: {SinhVien.MaLop}", fontInfo, Brushes.Black, left, currentY);
                    currentY += lineHeight + 10;
                }
            }
            else
            {
                // Tiêu đề thu gọn ở các trang tiếp theo
                using (var fontCont = new Font("Arial", 9.5f, FontStyle.Italic))
                {
                    e.Graphics.DrawString($"PHIẾU BÁO ĐIỂM - SV: {SinhVien.HoTen} ({SinhVien.MaSv}) - Lớp: {SinhVien.MaLop} (Tiếp theo)", fontCont, Brushes.Black, left, currentY);
                }
                currentY += 24;
            }

            // 2. Chia tỉ lệ các cột trong bảng điểm (đảm bảo co giãn theo MarginBounds.Width)
            float colSttWidth = Math.Max(50, width * 0.10f);
            float colTcWidth = Math.Max(70, width * 0.16f);
            float colDiemWidth = Math.Max(70, width * 0.18f);
            float colMonWidth = width - colSttWidth - colTcWidth - colDiemWidth;

            float rowHeight = 25;
            float tableHeaderHeight = 26;

            // Vẽ tiêu đề cột bảng điểm
            using (var fontHeader = new Font("Arial", 9.5f, FontStyle.Bold))
            using (var headerBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            using (var pen = new Pen(Color.Black, 1f))
            {
                RectangleF rectStt = new RectangleF(left, currentY, colSttWidth, tableHeaderHeight);
                RectangleF rectMon = new RectangleF(rectStt.Right, currentY, colMonWidth, tableHeaderHeight);
                RectangleF rectTc = new RectangleF(rectMon.Right, currentY, colTcWidth, tableHeaderHeight);
                RectangleF rectDiem = new RectangleF(rectTc.Right, currentY, colDiemWidth, tableHeaderHeight);

                e.Graphics.FillRectangle(headerBrush, rectStt);
                e.Graphics.FillRectangle(headerBrush, rectMon);
                e.Graphics.FillRectangle(headerBrush, rectTc);
                e.Graphics.FillRectangle(headerBrush, rectDiem);

                e.Graphics.DrawRectangle(pen, rectStt.X, rectStt.Y, rectStt.Width, rectStt.Height);
                e.Graphics.DrawRectangle(pen, rectMon.X, rectMon.Y, rectMon.Width, rectMon.Height);
                e.Graphics.DrawRectangle(pen, rectTc.X, rectTc.Y, rectTc.Width, rectTc.Height);
                e.Graphics.DrawRectangle(pen, rectDiem.X, rectDiem.Y, rectDiem.Width, rectDiem.Height);

                var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString("STT", fontHeader, Brushes.Black, rectStt, sfCenter);
                e.Graphics.DrawString("Môn học", fontHeader, Brushes.Black, rectMon, sfCenter);
                e.Graphics.DrawString("Số TC", fontHeader, Brushes.Black, rectTc, sfCenter);
                e.Graphics.DrawString("Điểm", fontHeader, Brushes.Black, rectDiem, sfCenter);
            }
            currentY += tableHeaderHeight;

            // 3. Vẽ các dòng dữ liệu môn học
            int summaryAndSigHeight = 135;
            float footerHeight = 35;

            using (var fontRow = new Font("Arial", 9.5f, FontStyle.Regular))
            using (var pen = new Pen(Color.Black, 1f))
            {
                var sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                var sfLeft = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

                while (_currentItemIndex < DiemMons.Count)
                {
                    bool isLastItem = (_currentItemIndex == DiemMons.Count - 1);
                    float neededHeight = isLastItem ? (rowHeight + summaryAndSigHeight + footerHeight) : (rowHeight + footerHeight);

                    if (currentY + neededHeight > bottom && _currentItemIndex > 0)
                    {
                        // Hết chỗ trên trang hiện tại -> sang trang tiếp theo
                        break;
                    }

                    var mon = DiemMons[_currentItemIndex];
                    RectangleF rectStt = new RectangleF(left, currentY, colSttWidth, rowHeight);
                    RectangleF rectMon = new RectangleF(rectStt.Right, currentY, colMonWidth, rowHeight);
                    RectangleF rectTc = new RectangleF(rectMon.Right, currentY, colTcWidth, rowHeight);
                    RectangleF rectDiem = new RectangleF(rectTc.Right, currentY, colDiemWidth, rowHeight);

                    e.Graphics.DrawRectangle(pen, rectStt.X, rectStt.Y, rectStt.Width, rectStt.Height);
                    e.Graphics.DrawRectangle(pen, rectMon.X, rectMon.Y, rectMon.Width, rectMon.Height);
                    e.Graphics.DrawRectangle(pen, rectTc.X, rectTc.Y, rectTc.Width, rectTc.Height);
                    e.Graphics.DrawRectangle(pen, rectDiem.X, rectDiem.Y, rectDiem.Width, rectDiem.Height);

                    e.Graphics.DrawString((_currentItemIndex + 1).ToString(), fontRow, Brushes.Black, rectStt, sfCenter);

                    RectangleF rectMonPadded = new RectangleF(rectMon.X + 8, rectMon.Y, rectMon.Width - 16, rectMon.Height);
                    e.Graphics.DrawString(mon.TenMon ?? "", fontRow, Brushes.Black, rectMonPadded, sfLeft);

                    e.Graphics.DrawString(mon.SoTinChi?.ToString() ?? "", fontRow, Brushes.Black, rectTc, sfCenter);

                    string diemStr = mon.Diem.HasValue ? mon.Diem.Value.ToString("0.0", _viCulture) : "";
                    e.Graphics.DrawString(diemStr, fontRow, Brushes.Black, rectDiem, sfCenter);

                    currentY += rowHeight;
                    _currentItemIndex++;
                }
            }

            bool hasMorePages = _currentItemIndex < DiemMons.Count;

            // 4. Nếu là trang cuối cùng: In điểm trung bình, xếp loại và 2 chữ ký
            if (!hasMorePages)
            {
                currentY += 18;

                // Dòng: Điểm trung bình: ... - Xếp loại: ...
                using (var fontSummary = new Font("Arial", 11, FontStyle.Bold))
                {
                    string dtbStr = DiemTB.ToString("0.00", _viCulture);
                    string summaryText = $"Điểm trung bình: {dtbStr} - Xếp loại: {XepLoai}";
                    var summarySize = e.Graphics.MeasureString(summaryText, fontSummary);
                    float summaryX = left + (width - summarySize.Width) / 2;
                    e.Graphics.DrawString(summaryText, fontSummary, Brushes.Black, summaryX, currentY);
                }
                currentY += 32;

                // Hai chỗ ký tên: Giáo vụ khoa và Sinh viên
                using (var fontSignTitle = new Font("Arial", 10.5f, FontStyle.Bold))
                using (var fontSignSub = new Font("Arial", 9.5f, FontStyle.Italic))
                {
                    var sfCenter = new StringFormat { Alignment = StringAlignment.Center };

                    // Cột trái: Giáo vụ khoa
                    float col1Center = left + width * 0.25f;
                    e.Graphics.DrawString("Giáo vụ khoa", fontSignTitle, Brushes.Black, col1Center, currentY, sfCenter);
                    e.Graphics.DrawString("(Ký, ghi rõ họ tên)", fontSignSub, Brushes.Black, col1Center, currentY + 20, sfCenter);

                    // Cột phải: Sinh viên
                    float col2Center = left + width * 0.75f;
                    e.Graphics.DrawString("Sinh viên", fontSignTitle, Brushes.Black, col2Center, currentY, sfCenter);
                    e.Graphics.DrawString("(Ký, ghi rõ họ tên)", fontSignSub, Brushes.Black, col2Center, currentY + 20, sfCenter);
                }
            }

            // 5. In số trang ở cuối: "Trang x/y"
            using (var fontFooter = new Font("Arial", 9.5f, FontStyle.Regular))
            {
                int displayTotal = Math.Max(_totalPages, _currentPage);
                string pageText = $"Trang {_currentPage}/{displayTotal}";
                var pageSize = e.Graphics.MeasureString(pageText, fontFooter);
                e.Graphics.DrawString(pageText, fontFooter, Brushes.Black, right - pageSize.Width, bottom - 18);
            }

            e.HasMorePages = hasMorePages;
        }
    }
}
