namespace Bai2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Top: Tìm kiếm hóa đơn
            gbTimKiem = new GroupBox();
            lblTuNgay = new Label();
            dtpTuNgay = new DateTimePicker();
            lblGach = new Label();
            lblDenNgay = new Label();
            dtpDenNgay = new DateTimePicker();
            lblTimSoBan = new Label();
            cboTimSoBan = new ComboBox();
            lblTimHinhThuc = new Label();
            cboTimHinhThuc = new ComboBox();
            lblTimTenMon = new Label();
            txtTimTenMon = new TextBox();
            btnTim = new Button();

            // Vùng trái: Danh sách hóa đơn
            lblDanhSachHoaDon = new Label();
            dgvHoaDon = new DataGridView();
            lblTongDoanhThu = new Label();

            // Vùng phải: Chi tiết hóa đơn
            gbChiTiet = new GroupBox();
            dtpNgayLap = new DateTimePicker();
            lblSoBan = new Label();
            cboSoBan = new ComboBox();
            radTienMat = new RadioButton();
            radChuyenKhoan = new RadioButton();
            lblGhiChu = new Label();
            txtGhiChu = new TextBox();
            lblChonMon = new Label();
            cboMon = new ComboBox();
            lblSL = new Label();
            nudSoLuong = new NumericUpDown();
            btnThemMon = new Button();
            dgvChiTiet = new DataGridView();
            lblTongCong = new Label();

            // Bottom buttons
            btnHoaDonMoi = new Button();
            btnSuaHoaDon = new Button();
            btnXoaHoaDon = new Button();
            btnSuaSL = new Button();
            btnXoaMon = new Button();
            btnXemHoaDon = new Button();
            btnXuatPdf = new Button();
            btnIn = new Button();

            gbTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
            gbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
            SuspendLayout();

            // 
            // gbTimKiem
            // 
            gbTimKiem.Controls.Add(btnTim);
            gbTimKiem.Controls.Add(txtTimTenMon);
            gbTimKiem.Controls.Add(lblTimTenMon);
            gbTimKiem.Controls.Add(cboTimHinhThuc);
            gbTimKiem.Controls.Add(lblTimHinhThuc);
            gbTimKiem.Controls.Add(cboTimSoBan);
            gbTimKiem.Controls.Add(lblTimSoBan);
            gbTimKiem.Controls.Add(dtpDenNgay);
            gbTimKiem.Controls.Add(lblDenNgay);
            gbTimKiem.Controls.Add(lblGach);
            gbTimKiem.Controls.Add(dtpTuNgay);
            gbTimKiem.Controls.Add(lblTuNgay);
            gbTimKiem.Font = new Font("Segoe UI", 9.5F);
            gbTimKiem.Location = new Point(14, 10);
            gbTimKiem.Name = "gbTimKiem";
            gbTimKiem.Size = new Size(1110, 80);
            gbTimKiem.TabIndex = 0;
            gbTimKiem.TabStop = false;
            gbTimKiem.Text = "Tìm kiếm hóa đơn";

            // lblTuNgay
            lblTuNgay.AutoSize = true;
            lblTuNgay.Location = new Point(12, 24);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(58, 17);
            lblTuNgay.TabIndex = 0;
            lblTuNgay.Text = "Từ ngày";

            // dtpTuNgay
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(12, 44);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(115, 24);
            dtpTuNgay.TabIndex = 1;

            // lblGach
            lblGach.AutoSize = true;
            lblGach.Location = new Point(132, 48);
            lblGach.Name = "lblGach";
            lblGach.Size = new Size(13, 17);
            lblGach.TabIndex = 2;
            lblGach.Text = "-";

            // lblDenNgay
            lblDenNgay.AutoSize = true;
            lblDenNgay.Location = new Point(152, 24);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(66, 17);
            lblDenNgay.TabIndex = 3;
            lblDenNgay.Text = "Đến ngày";

            // dtpDenNgay
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(152, 44);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(115, 24);
            dtpDenNgay.TabIndex = 4;

            // lblTimSoBan
            lblTimSoBan.AutoSize = true;
            lblTimSoBan.Location = new Point(285, 24);
            lblTimSoBan.Name = "lblTimSoBan";
            lblTimSoBan.Size = new Size(50, 17);
            lblTimSoBan.TabIndex = 5;
            lblTimSoBan.Text = "Số bàn";

            // cboTimSoBan
            cboTimSoBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimSoBan.FormattingEnabled = true;
            cboTimSoBan.Location = new Point(285, 44);
            cboTimSoBan.Name = "cboTimSoBan";
            cboTimSoBan.Size = new Size(110, 24);
            cboTimSoBan.TabIndex = 6;

            // lblTimHinhThuc
            lblTimHinhThuc.AutoSize = true;
            lblTimHinhThuc.Location = new Point(415, 24);
            lblTimHinhThuc.Name = "lblTimHinhThuc";
            lblTimHinhThuc.Size = new Size(63, 17);
            lblTimHinhThuc.TabIndex = 7;
            lblTimHinhThuc.Text = "Hình thức";

            // cboTimHinhThuc
            cboTimHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimHinhThuc.FormattingEnabled = true;
            cboTimHinhThuc.Location = new Point(415, 44);
            cboTimHinhThuc.Name = "cboTimHinhThuc";
            cboTimHinhThuc.Size = new Size(115, 24);
            cboTimHinhThuc.TabIndex = 8;

            // lblTimTenMon
            lblTimTenMon.AutoSize = true;
            lblTimTenMon.Location = new Point(550, 24);
            lblTimTenMon.Name = "lblTimTenMon";
            lblTimTenMon.Size = new Size(60, 17);
            lblTimTenMon.TabIndex = 9;
            lblTimTenMon.Text = "Tên món";

            // txtTimTenMon
            txtTimTenMon.Location = new Point(550, 44);
            txtTimTenMon.Name = "txtTimTenMon";
            txtTimTenMon.Size = new Size(180, 24);
            txtTimTenMon.TabIndex = 10;

            // btnTim
            btnTim.Location = new Point(745, 42);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(80, 28);
            btnTim.TabIndex = 11;
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = true;

            // 
            // lblDanhSachHoaDon
            // 
            lblDanhSachHoaDon.AutoSize = true;
            lblDanhSachHoaDon.Font = new Font("Segoe UI", 9.5F);
            lblDanhSachHoaDon.Location = new Point(14, 98);
            lblDanhSachHoaDon.Name = "lblDanhSachHoaDon";
            lblDanhSachHoaDon.Size = new Size(122, 17);
            lblDanhSachHoaDon.TabIndex = 1;
            lblDanhSachHoaDon.Text = "Danh sách hóa đơn";

            // 
            // dgvHoaDon
            // 
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToDeleteRows = false;
            dgvHoaDon.BackgroundColor = SystemColors.ControlLight;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaDon.Location = new Point(14, 122);
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.RowHeadersWidth = 24;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.Size = new Size(500, 390);
            dgvHoaDon.TabIndex = 2;

            // 
            // lblTongDoanhThu
            // 
            lblTongDoanhThu.AutoSize = true;
            lblTongDoanhThu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongDoanhThu.Location = new Point(14, 524);
            lblTongDoanhThu.Name = "lblTongDoanhThu";
            lblTongDoanhThu.Size = new Size(385, 25);
            lblTongDoanhThu.TabIndex = 3;
            lblTongDoanhThu.Text = "Tổng doanh thu đang hiển thị: 0 đ";

            // 
            // gbChiTiet
            // 
            gbChiTiet.Controls.Add(btnThemMon);
            gbChiTiet.Controls.Add(nudSoLuong);
            gbChiTiet.Controls.Add(lblSL);
            gbChiTiet.Controls.Add(cboMon);
            gbChiTiet.Controls.Add(lblChonMon);
            gbChiTiet.Controls.Add(txtGhiChu);
            gbChiTiet.Controls.Add(lblGhiChu);
            gbChiTiet.Controls.Add(radChuyenKhoan);
            gbChiTiet.Controls.Add(radTienMat);
            gbChiTiet.Controls.Add(cboSoBan);
            gbChiTiet.Controls.Add(lblSoBan);
            gbChiTiet.Controls.Add(dtpNgayLap);
            gbChiTiet.Controls.Add(dgvChiTiet);
            gbChiTiet.Font = new Font("Segoe UI", 9.5F);
            gbChiTiet.Location = new Point(530, 98);
            gbChiTiet.Name = "gbChiTiet";
            gbChiTiet.Size = new Size(594, 414);
            gbChiTiet.TabIndex = 4;
            gbChiTiet.TabStop = false;
            gbChiTiet.Text = "Chi tiết hóa đơn";

            // dtpNgayLap
            dtpNgayLap.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpNgayLap.Format = DateTimePickerFormat.Custom;
            dtpNgayLap.Location = new Point(12, 28);
            dtpNgayLap.Name = "dtpNgayLap";
            dtpNgayLap.Size = new Size(140, 24);
            dtpNgayLap.TabIndex = 0;

            // lblSoBan
            lblSoBan.AutoSize = true;
            lblSoBan.Location = new Point(165, 31);
            lblSoBan.Name = "lblSoBan";
            lblSoBan.Size = new Size(52, 17);
            lblSoBan.TabIndex = 1;
            lblSoBan.Text = "Số bàn:";

            // cboSoBan
            cboSoBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSoBan.FormattingEnabled = true;
            cboSoBan.Location = new Point(220, 28);
            cboSoBan.Name = "cboSoBan";
            cboSoBan.Size = new Size(60, 24);
            cboSoBan.TabIndex = 2;

            // radTienMat
            radTienMat.AutoSize = true;
            radTienMat.Checked = true;
            radTienMat.Location = new Point(295, 30);
            radTienMat.Name = "radTienMat";
            radTienMat.Size = new Size(76, 21);
            radTienMat.TabIndex = 3;
            radTienMat.TabStop = true;
            radTienMat.Text = "Tiền mặt";
            radTienMat.UseVisualStyleBackColor = true;

            // radChuyenKhoan
            radChuyenKhoan.AutoSize = true;
            radChuyenKhoan.Location = new Point(380, 30);
            radChuyenKhoan.Name = "radChuyenKhoan";
            radChuyenKhoan.Size = new Size(108, 21);
            radChuyenKhoan.TabIndex = 4;
            radChuyenKhoan.Text = "Chuyển khoản";
            radChuyenKhoan.UseVisualStyleBackColor = true;

            // lblGhiChu
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(12, 65);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(54, 17);
            lblGhiChu.TabIndex = 5;
            lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            txtGhiChu.Location = new Point(80, 62);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.ScrollBars = ScrollBars.Vertical;
            txtGhiChu.Size = new Size(500, 48);
            txtGhiChu.TabIndex = 6;

            // lblChonMon
            lblChonMon.AutoSize = true;
            lblChonMon.Location = new Point(12, 122);
            lblChonMon.Name = "lblChonMon";
            lblChonMon.Size = new Size(71, 17);
            lblChonMon.TabIndex = 7;
            lblChonMon.Text = "Chọn món:";

            // cboMon
            cboMon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMon.FormattingEnabled = true;
            cboMon.Location = new Point(88, 119);
            cboMon.Name = "cboMon";
            cboMon.Size = new Size(210, 24);
            cboMon.TabIndex = 8;

            // lblSL
            lblSL.AutoSize = true;
            lblSL.Location = new Point(310, 122);
            lblSL.Name = "lblSL:";
            lblSL.Size = new Size(24, 17);
            lblSL.TabIndex = 9;
            lblSL.Text = "SL:";

            // nudSoLuong
            nudSoLuong.Location = new Point(340, 120);
            nudSoLuong.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(50, 24);
            nudSoLuong.TabIndex = 10;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // btnThemMon
            btnThemMon.Location = new Point(405, 118);
            btnThemMon.Name = "btnThemMon";
            btnThemMon.Size = new Size(88, 28);
            btnThemMon.TabIndex = 11;
            btnThemMon.Text = "Thêm món";
            btnThemMon.UseVisualStyleBackColor = true;

            // dgvChiTiet
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.BackgroundColor = SystemColors.ControlLight;
            dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTiet.Location = new Point(12, 155);
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.Name = "dgvChiTiet";
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.RowHeadersWidth = 24;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.Size = new Size(568, 245);
            dgvChiTiet.TabIndex = 12;

            // 
            // lblTongCong
            // 
            lblTongCong.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTongCong.Location = new Point(600, 520);
            lblTongCong.Name = "lblTongCong";
            lblTongCong.Size = new Size(520, 32);
            lblTongCong.TabIndex = 5;
            lblTongCong.Text = "Tổng cộng: 0 đ";
            lblTongCong.TextAlign = ContentAlignment.MiddleRight;

            // 
            // Bottom Buttons
            // 
            btnHoaDonMoi.Font = new Font("Segoe UI", 9.5F);
            btnHoaDonMoi.Location = new Point(14, 565);
            btnHoaDonMoi.Name = "btnHoaDonMoi";
            btnHoaDonMoi.Size = new Size(110, 32);
            btnHoaDonMoi.TabIndex = 6;
            btnHoaDonMoi.Text = "Hóa đơn mới";
            btnHoaDonMoi.UseVisualStyleBackColor = true;

            btnSuaHoaDon.Font = new Font("Segoe UI", 9.5F);
            btnSuaHoaDon.Location = new Point(132, 565);
            btnSuaHoaDon.Name = "btnSuaHoaDon";
            btnSuaHoaDon.Size = new Size(110, 32);
            btnSuaHoaDon.TabIndex = 7;
            btnSuaHoaDon.Text = "Sửa hóa đơn";
            btnSuaHoaDon.UseVisualStyleBackColor = true;

            btnXoaHoaDon.Font = new Font("Segoe UI", 9.5F);
            btnXoaHoaDon.Location = new Point(250, 565);
            btnXoaHoaDon.Name = "btnXoaHoaDon";
            btnXoaHoaDon.Size = new Size(110, 32);
            btnXoaHoaDon.TabIndex = 8;
            btnXoaHoaDon.Text = "Xóa hóa đơn";
            btnXoaHoaDon.UseVisualStyleBackColor = true;

            btnSuaSL.Font = new Font("Segoe UI", 9.5F);
            btnSuaSL.Location = new Point(410, 565);
            btnSuaSL.Name = "btnSuaSL";
            btnSuaSL.Size = new Size(85, 32);
            btnSuaSL.TabIndex = 9;
            btnSuaSL.Text = "Sửa SL";
            btnSuaSL.UseVisualStyleBackColor = true;

            btnXoaMon.Font = new Font("Segoe UI", 9.5F);
            btnXoaMon.Location = new Point(505, 565);
            btnXoaMon.Name = "btnXoaMon";
            btnXoaMon.Size = new Size(85, 32);
            btnXoaMon.TabIndex = 10;
            btnXoaMon.Text = "Xóa món";
            btnXoaMon.UseVisualStyleBackColor = true;

            btnXemHoaDon.Font = new Font("Segoe UI", 9.5F);
            btnXemHoaDon.Location = new Point(670, 565);
            btnXemHoaDon.Name = "btnXemHoaDon";
            btnXemHoaDon.Size = new Size(115, 32);
            btnXemHoaDon.TabIndex = 11;
            btnXemHoaDon.Text = "Xem hóa đơn";
            btnXemHoaDon.UseVisualStyleBackColor = true;

            btnXuatPdf.Font = new Font("Segoe UI", 9.5F);
            btnXuatPdf.Location = new Point(795, 565);
            btnXuatPdf.Name = "btnXuatPdf";
            btnXuatPdf.Size = new Size(95, 32);
            btnXuatPdf.TabIndex = 12;
            btnXuatPdf.Text = "Xuất PDF";
            btnXuatPdf.UseVisualStyleBackColor = true;

            btnIn.Font = new Font("Segoe UI", 9.5F);
            btnIn.Location = new Point(900, 565);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(75, 32);
            btnIn.TabIndex = 13;
            btnIn.Text = "In";
            btnIn.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 615);
            Controls.Add(btnIn);
            Controls.Add(btnXuatPdf);
            Controls.Add(btnXemHoaDon);
            Controls.Add(btnXoaMon);
            Controls.Add(btnSuaSL);
            Controls.Add(btnXoaHoaDon);
            Controls.Add(btnSuaHoaDon);
            Controls.Add(btnHoaDonMoi);
            Controls.Add(lblTongCong);
            Controls.Add(gbChiTiet);
            Controls.Add(lblTongDoanhThu);
            Controls.Add(dgvHoaDon);
            Controls.Add(lblDanhSachHoaDon);
            Controls.Add(gbTimKiem);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quán cà phê Góc Phố - Quản lý hóa đơn";
            Load += Form1_Load;
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
            gbChiTiet.ResumeLayout(false);
            gbChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox gbTimKiem;
        private Label lblTuNgay;
        private DateTimePicker dtpTuNgay;
        private Label lblGach;
        private Label lblDenNgay;
        private DateTimePicker dtpDenNgay;
        private Label lblTimSoBan;
        private ComboBox cboTimSoBan;
        private Label lblTimHinhThuc;
        private ComboBox cboTimHinhThuc;
        private Label lblTimTenMon;
        private TextBox txtTimTenMon;
        private Button btnTim;
        private Label lblDanhSachHoaDon;
        private DataGridView dgvHoaDon;
        private Label lblTongDoanhThu;
        private GroupBox gbChiTiet;
        private DateTimePicker dtpNgayLap;
        private Label lblSoBan;
        private ComboBox cboSoBan;
        private RadioButton radTienMat;
        private RadioButton radChuyenKhoan;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private Label lblChonMon;
        private ComboBox cboMon;
        private Label lblSL;
        private NumericUpDown nudSoLuong;
        private Button btnThemMon;
        private DataGridView dgvChiTiet;
        private Label lblTongCong;
        private Button btnHoaDonMoi;
        private Button btnSuaHoaDon;
        private Button btnXoaHoaDon;
        private Button btnSuaSL;
        private Button btnXoaMon;
        private Button btnXemHoaDon;
        private Button btnXuatPdf;
        private Button btnIn;
    }
}
