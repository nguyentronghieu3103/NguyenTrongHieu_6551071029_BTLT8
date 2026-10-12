using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bai3.Models;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace Bai3
{
    public static class TestRunner
    {
        public static void RunAllTests()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("STARTING VERIFICATION TESTS");
            Console.WriteLine("========================================");

            using var context = new QuanLyDiemContext();

            // 1. Test database connection & student count
            var allStudents = context.SinhViens.Include(s => s.DiemMons).OrderBy(s => s.MaSv).ToList();
            Console.WriteLine($"[TEST 1] Total students in DB: {allStudents.Count} (Expected: 3)");
            if (allStudents.Count != 3) throw new Exception("Expected 3 students in DB!");

            // 2. Test class filtering
            var cntt01 = allStudents.Where(s => s.MaLop == "CNTT01").ToList();
            Console.WriteLine($"[TEST 2] CNTT01 students: {cntt01.Count} (Expected: 2)");
            if (cntt01.Count != 2) throw new Exception("Expected 2 students in CNTT01!");

            var kt01 = allStudents.Where(s => s.MaLop == "KT01").ToList();
            Console.WriteLine($"[TEST 3] KT01 students: {kt01.Count} (Expected: 1)");
            if (kt01.Count != 1) throw new Exception("Expected 1 student in KT01!");

            // 3. Test search keyword "anh" (case-insensitive)
            string keyword = "anh".ToLower();
            var searchResults = allStudents.Where(s => s.MaSv.ToLower().Contains(keyword) || (s.HoTen != null && s.HoTen.ToLower().Contains(keyword))).ToList();
            Console.WriteLine($"[TEST 4] Search 'anh': {searchResults.Count} student(s) -> {string.Join(", ", searchResults.Select(s => s.HoTen))} (Expected: Nguyễn Minh Anh)");
            if (searchResults.Count != 1 || searchResults[0].MaSv != "SV001") throw new Exception("Search 'anh' failed!");

            // 4. Test score calculations
            // SV001
            var sv1 = allStudents.First(s => s.MaSv == "SV001");
            int tc1 = sv1.DiemMons.Sum(d => d.SoTinChi ?? 0);
            decimal tongDiem1 = sv1.DiemMons.Sum(d => (d.Diem ?? 0) * (d.SoTinChi ?? 0));
            decimal dtb1 = Math.Round(tongDiem1 / tc1, 2, MidpointRounding.AwayFromZero);
            string xl1 = dtb1 >= 8.0m ? "Giỏi" : (dtb1 >= 7.0m ? "Khá" : (dtb1 >= 5.0m ? "Trung bình" : "Yếu"));
            Console.WriteLine($"[TEST 5] SV001: {sv1.DiemMons.Count} môn, {tc1} TC, ĐTB {dtb1:0.00}, Xếp loại: {xl1} (Expected: 4 môn, 11 TC, 7.77, Khá)");
            if (tc1 != 11 || dtb1 != 7.77m || xl1 != "Khá") throw new Exception("SV001 stats mismatch!");

            // SV002
            var sv2 = allStudents.First(s => s.MaSv == "SV002");
            int tc2 = sv2.DiemMons.Sum(d => d.SoTinChi ?? 0);
            decimal tongDiem2 = sv2.DiemMons.Sum(d => (d.Diem ?? 0) * (d.SoTinChi ?? 0));
            decimal dtb2 = Math.Round(tongDiem2 / tc2, 2, MidpointRounding.AwayFromZero);
            string xl2 = dtb2 >= 8.0m ? "Giỏi" : (dtb2 >= 7.0m ? "Khá" : (dtb2 >= 5.0m ? "Trung bình" : "Yếu"));
            Console.WriteLine($"[TEST 6] SV002: {sv2.DiemMons.Count} môn, {tc2} TC, ĐTB {dtb2:0.00}, Xếp loại: {xl2} (Expected: 3 môn, 8 TC, 8.50, Giỏi)");
            if (tc2 != 8 || dtb2 != 8.50m || xl2 != "Giỏi") throw new Exception("SV002 stats mismatch!");

            // SV003
            var sv3 = allStudents.First(s => s.MaSv == "SV003");
            Console.WriteLine($"[TEST 7] SV003 môn count: {sv3.DiemMons.Count} (Expected: 0 môn / Chưa có điểm)");
            if (sv3.DiemMons.Count != 0) throw new Exception("SV003 should have 0 subjects!");

            // 5. Test PhieuDiemPrinter pagination with 4 môn and with 40 môn
            var printer1 = new PhieuDiemPrinter(sv1, sv1.DiemMons.ToList(), dtb1, xl1);
            // Simulate print start to test total pages calculation
            var printer1Type = typeof(PhieuDiemPrinter);
            var calcMethod = printer1Type.GetMethod("CalculateTotalPages", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            int pagesFor4 = (int)calcMethod!.Invoke(printer1, new object[] { 900, 4 })!;
            Console.WriteLine($"[TEST 8] Printer pages for 4 subjects: {pagesFor4} (Expected: 1)");
            if (pagesFor4 != 1) throw new Exception("Expected 1 page for 4 subjects!");

            // Test with 40 subjects
            var mock40Subjects = new List<DiemMon>();
            for (int i = 1; i <= 40; i++)
            {
                mock40Subjects.Add(new DiemMon { MaDiem = 100 + i, MaSv = "SV001", TenMon = $"Môn học thử nghiệm {i}", SoTinChi = 3, Diem = 8.0m });
            }
            int pagesFor40 = (int)calcMethod!.Invoke(printer1, new object[] { 900, 40 })!;
            Console.WriteLine($"[TEST 9] Printer pages for 40 subjects: {pagesFor40} (Expected: 2)");
            if (pagesFor40 != 2) throw new Exception("Expected 2 pages for 40 subjects!");

            // 6. Test Excel Export logic with ClosedXML
            var exportData = (from sv in context.SinhViens
                              join lp in context.Lops on sv.MaLop equals lp.MaLop
                              join dm in context.DiemMons on sv.MaSv equals dm.MaSv
                              where lp.MaLop == "CNTT01"
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

            Console.WriteLine($"[TEST 10] Excel export rows for CNTT01: {exportData.Count} (Expected: 7)");
            if (exportData.Count != 7) throw new Exception("Expected 7 rows for CNTT01!");

            string testExcelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test_BangDiem_CNTT01.xlsx");
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Điểm_CNTT01");
                ws.Cell("A1").Value = "BẢNG ĐIỂM LỚP CNTT01";
                int headerRow = 3;
                string[] headers = { "MSSV", "Họ tên", "Lớp", "Môn học", "Số TC", "Điểm" };
                for (int i = 0; i < headers.Length; i++)
                {
                    ws.Cell(headerRow, i + 1).Value = headers[i];
                }

                int dataStartRow = headerRow + 1;
                int currentRow = dataStartRow;
                foreach (var item in exportData)
                {
                    ws.Cell(currentRow, 1).Value = item.MaSv;
                    ws.Cell(currentRow, 2).Value = item.HoTen;
                    ws.Cell(currentRow, 3).Value = item.MaLop;
                    ws.Cell(currentRow, 4).Value = item.TenMon;
                    ws.Cell(currentRow, 5).Value = item.SoTinChi ?? 0;
                    ws.Cell(currentRow, 6).Value = (double)(item.Diem ?? 0);
                    currentRow++;
                }

                int dataEndRow = currentRow - 1;
                // Sum
                ws.Cell(currentRow, 1).Value = "Tổng số tín chỉ";
                ws.Cell(currentRow, 5).FormulaA1 = $"SUM(E{dataStartRow}:E{dataEndRow})";
                currentRow++;
                // Average
                ws.Cell(currentRow, 1).Value = "Điểm trung bình";
                ws.Cell(currentRow, 6).FormulaA1 = $"AVERAGE(F{dataStartRow}:F{dataEndRow})";

                wb.SaveAs(testExcelPath);
            }

            // Verify the generated Excel formulas and evaluated values
            using (var readWb = new XLWorkbook(testExcelPath))
            {
                var ws = readWb.Worksheet("Điểm_CNTT01");
                string sumFormula = ws.Cell("E11").FormulaA1;
                string avgFormula = ws.Cell("F12").FormulaA1;
                double sumVal = ws.Cell("E11").GetDouble();
                double avgVal = Math.Round(ws.Cell("F12").GetDouble(), 2);

                Console.WriteLine($"[TEST 11] Excel SUM formula: {sumFormula}, Value: {sumVal} (Expected: SUM(E4:E10), 19)");
                Console.WriteLine($"[TEST 12] Excel AVERAGE formula: {avgFormula}, Value: {avgVal} (Expected: AVERAGE(F4:F10), 8.14)");

                if (sumFormula != "SUM(E4:E10)" || sumVal != 19) throw new Exception("Excel SUM formula failed!");
                if (avgFormula != "AVERAGE(F4:F10)" || Math.Abs(avgVal - 8.14) > 0.01) throw new Exception("Excel AVERAGE formula failed!");
            }

            // Clean up test file
            if (File.Exists(testExcelPath)) File.Delete(testExcelPath);

            Console.WriteLine("========================================");
            Console.WriteLine("ALL 12 TESTS PASSED PERFECTLY!");
            Console.WriteLine("========================================");
        }
    }
}
