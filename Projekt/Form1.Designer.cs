using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Projekt
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            generate_button = new Button();
            import_button = new Button();
            graph_box = new PictureBox();
            province_input = new ComboBox();
            label1 = new Label();
            data_type_input = new ComboBox();
            label2 = new Label();
            file_format_input = new ComboBox();
            label3 = new Label();
            export_button = new Button();
            file_name_input = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            login_button = new Button();
            email_input = new TextBox();
            password_input = new TextBox();
            register_button = new Button();
            label7 = new Label();
            user_info = new Label();
            label8 = new Label();
            data_info = new Label();
            database_save_button = new Button();
            database_read_button = new Button();
            label9 = new Label();
            data_id = new TextBox();
            label10 = new Label();
            load_data_button = new Button();
            ((System.ComponentModel.ISupportInitialize)graph_box).BeginInit();
            SuspendLayout();
            // 
            // generate_button
            // 
            generate_button.Location = new Point(608, 157);
            generate_button.Name = "generate_button";
            generate_button.Size = new Size(121, 23);
            generate_button.TabIndex = 0;
            generate_button.Text = "Generuj wykres";
            generate_button.UseVisualStyleBackColor = true;
            generate_button.Click += generate_button_click;
            // 
            // import_button
            // 
            import_button.Location = new Point(631, 283);
            import_button.Name = "import_button";
            import_button.Size = new Size(75, 23);
            import_button.TabIndex = 1;
            import_button.Text = "Importuj";
            import_button.UseVisualStyleBackColor = true;
            import_button.Click += import_button_click;
            // 
            // graph_box
            // 
            graph_box.Location = new Point(12, 12);
            graph_box.Name = "graph_box";
            graph_box.Size = new Size(538, 319);
            graph_box.TabIndex = 2;
            graph_box.TabStop = false;
            // 
            // province_input
            // 
            province_input.DropDownStyle = ComboBoxStyle.DropDownList;
            province_input.FormattingEnabled = true;
            province_input.Items.AddRange(new object[] { "Dolnośląskie", "Kujawsko-Pomorskie", "Lubelskie", "Lubuskie", "Łódzkie", "Małopolskie", "Mazowieckie", "Opolskie", "Podkarpackie", "Podlaskie", "Pomorskie", "Śląskie", "Świętokrzyskie", "Warmińsko-Mazurskie", "Wielkopolskie", "Zachodniopomorskie" });
            province_input.Location = new Point(588, 32);
            province_input.Name = "province_input";
            province_input.Size = new Size(161, 23);
            province_input.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(626, 14);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Województwo";
            // 
            // data_type_input
            // 
            data_type_input.DropDownStyle = ComboBoxStyle.DropDownList;
            data_type_input.FormattingEnabled = true;
            data_type_input.Items.AddRange(new object[] { "Inflacja (%)", "Bezrobocie (%)", "Miesięczne zarobki (PLN)" });
            data_type_input.Location = new Point(588, 88);
            data_type_input.Name = "data_type_input";
            data_type_input.Size = new Size(161, 23);
            data_type_input.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(611, 70);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 6;
            label2.Text = "Dane porównawcze";
            // 
            // file_format_input
            // 
            file_format_input.DropDownStyle = ComboBoxStyle.DropDownList;
            file_format_input.FormattingEnabled = true;
            file_format_input.Items.AddRange(new object[] { "XML", "JSON" });
            file_format_input.Location = new Point(608, 245);
            file_format_input.Name = "file_format_input";
            file_format_input.Size = new Size(121, 23);
            file_format_input.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(631, 227);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 8;
            label3.Text = "Format pliku";
            // 
            // export_button
            // 
            export_button.Location = new Point(631, 312);
            export_button.Name = "export_button";
            export_button.Size = new Size(75, 23);
            export_button.TabIndex = 9;
            export_button.Text = "Eksportuj";
            export_button.UseVisualStyleBackColor = true;
            export_button.Click += export_button_click;
            // 
            // file_name_input
            // 
            file_name_input.Location = new Point(618, 201);
            file_name_input.Name = "file_name_input";
            file_name_input.Size = new Size(100, 23);
            file_name_input.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(635, 183);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 11;
            label4.Text = "Nazwa pliku";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(54, 387);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 12;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(201, 387);
            label6.Name = "label6";
            label6.Size = new Size(37, 15);
            label6.TabIndex = 13;
            label6.Text = "Hasło";
            // 
            // login_button
            // 
            login_button.Location = new Point(434, 405);
            login_button.Name = "login_button";
            login_button.Size = new Size(75, 23);
            login_button.TabIndex = 16;
            login_button.Text = "Zaloguj się";
            login_button.UseVisualStyleBackColor = true;
            login_button.Click += login_button_click;
            // 
            // email_input
            // 
            email_input.Location = new Point(12, 405);
            email_input.Name = "email_input";
            email_input.Size = new Size(125, 23);
            email_input.TabIndex = 17;
            email_input.Text = "adam@gmail.com";
            email_input.TextChanged += email_input_TextChanged;
            // 
            // password_input
            // 
            password_input.Location = new Point(161, 405);
            password_input.Name = "password_input";
            password_input.Size = new Size(125, 23);
            password_input.TabIndex = 18;
            password_input.Text = "asdasd";
            // 
            // register_button
            // 
            register_button.Location = new Point(318, 404);
            register_button.Name = "register_button";
            register_button.Size = new Size(94, 23);
            register_button.TabIndex = 19;
            register_button.Text = "Zarejestruj się";
            register_button.UseVisualStyleBackColor = true;
            register_button.Click += register_button_click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 364);
            label7.Name = "label7";
            label7.Size = new Size(119, 15);
            label7.TabIndex = 20;
            label7.Text = "Aktywny użytkownik:";
            // 
            // user_info
            // 
            user_info.AutoSize = true;
            user_info.Location = new Point(137, 364);
            user_info.Name = "user_info";
            user_info.Size = new Size(30, 15);
            user_info.TabIndex = 21;
            user_info.Text = "brak";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 340);
            label8.Name = "label8";
            label8.Size = new Size(75, 15);
            label8.TabIndex = 22;
            label8.Text = "Stan danych:";
            label8.Click += label8_Click;
            // 
            // data_info
            // 
            data_info.AutoSize = true;
            data_info.Location = new Point(94, 340);
            data_info.Name = "data_info";
            data_info.Size = new Size(36, 15);
            data_info.TabIndex = 23;
            data_info.Text = "puste";
            // 
            // database_save_button
            // 
            database_save_button.Location = new Point(588, 371);
            database_save_button.Name = "database_save_button";
            database_save_button.Size = new Size(75, 23);
            database_save_button.TabIndex = 24;
            database_save_button.Text = "Save";
            database_save_button.UseVisualStyleBackColor = true;
            database_save_button.Click += database_save_button_click;
            // 
            // database_read_button
            // 
            database_read_button.Location = new Point(674, 371);
            database_read_button.Name = "database_read_button";
            database_read_button.Size = new Size(75, 23);
            database_read_button.TabIndex = 25;
            database_read_button.Text = "Read";
            database_read_button.UseVisualStyleBackColor = true;
            database_read_button.Click += database_read_button_click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(639, 351);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 26;
            label9.Text = "Database";
            // 
            // data_id
            // 
            data_id.Location = new Point(674, 404);
            data_id.Name = "data_id";
            data_id.Size = new Size(74, 23);
            data_id.TabIndex = 27;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(626, 409);
            label10.Name = "label10";
            label10.Size = new Size(47, 15);
            label10.TabIndex = 28;
            label10.Text = "Data id:";
            // 
            // load_data_button
            // 
            load_data_button.Location = new Point(608, 128);
            load_data_button.Name = "load_data_button";
            load_data_button.Size = new Size(121, 23);
            load_data_button.TabIndex = 29;
            load_data_button.Text = "Wczytaj dane";
            load_data_button.UseVisualStyleBackColor = true;
            load_data_button.Click += load_data_button_click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(load_data_button);
            Controls.Add(label10);
            Controls.Add(data_id);
            Controls.Add(label9);
            Controls.Add(database_read_button);
            Controls.Add(database_save_button);
            Controls.Add(data_info);
            Controls.Add(label8);
            Controls.Add(user_info);
            Controls.Add(label7);
            Controls.Add(register_button);
            Controls.Add(password_input);
            Controls.Add(email_input);
            Controls.Add(login_button);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(file_name_input);
            Controls.Add(export_button);
            Controls.Add(label3);
            Controls.Add(file_format_input);
            Controls.Add(label2);
            Controls.Add(data_type_input);
            Controls.Add(label1);
            Controls.Add(province_input);
            Controls.Add(graph_box);
            Controls.Add(import_button);
            Controls.Add(generate_button);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)graph_box).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button generate_button;
        private Button import_button;
        private PictureBox graph_box;
        private ComboBox province_input;
        private Label label1;
        private ComboBox data_type_input;
        private Label label2;
        private ComboBox file_format_input;
        private Label label3;
        private Button export_button;
        private TextBox file_name_input;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button login_button;
        private TextBox email_input;
        private TextBox password_input;
        private Button register_button;
        private Label label7;
        private Label user_info;
        private Label label8;
        private Label data_info;
        private Button database_save_button;
        private Button database_read_button;
        private Label label9;
        private TextBox data_id;
        private Label label10;
        private Button load_data_button;
    }
}