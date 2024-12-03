namespace курсовая
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

        private void InitializeComponent()
        {
            txtEqs = new TextBox();
            txtVars = new TextBox();
            btnGenSLAU = new Button();
            txtConverg = new TextBox();
            btnSolve = new Button();
            btnAddEq = new Button();
            results = new TextBox();
            btnAddVar = new Button();
            btnSave = new Button();
            btnLoad = new Button();
            SuspendLayout();
            
            txtEqs.Location = new Point(12, 12);
            txtEqs.Name = "txtEqs";
            txtEqs.PlaceholderText = "Число уравнений";
            txtEqs.Size = new Size(158, 31);
            txtEqs.TabIndex = 0;
            
            txtVars.Location = new Point(176, 12);
            txtVars.Name = "txtVars";
            txtVars.PlaceholderText = "Число неизвестных";
            txtVars.Size = new Size(170, 31);
            txtVars.TabIndex = 1;
             
            btnGenSLAU.Location = new Point(352, 12);
            btnGenSLAU.Name = "btnGenSLAU";
            btnGenSLAU.Size = new Size(161, 31);
            btnGenSLAU.TabIndex = 2;
            btnGenSLAU.Text = "Сгенерировать";
            btnGenSLAU.UseVisualStyleBackColor = true;
            btnGenSLAU.Click += btnGenSLAU_Click;
             
            txtConverg.Location = new Point(12, 49);
            txtConverg.Name = "txtConverg";
            txtConverg.PlaceholderText = "Точность";
            txtConverg.Size = new Size(100, 31);
            txtConverg.TabIndex = 3;
            
            btnSolve.Location = new Point(118, 49);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new Size(139, 31);
            btnSolve.TabIndex = 4;
            btnSolve.Text = "Рассчитать";
            btnSolve.UseVisualStyleBackColor = true;
            btnSolve.Click += btnSolve_Click;
            
            btnAddEq.Location = new Point(521, 12);
            btnAddEq.Name = "btnAddEq";
            btnAddEq.Size = new Size(224, 68);
            btnAddEq.TabIndex = 6;
            btnAddEq.Text = "Добавить недостающие уравнения";
            btnAddEq.UseVisualStyleBackColor = true;
            btnAddEq.Click += btnAddEq_Click;
            
            results.Location = new Point(12, 324);
            results.Multiline = true;
            results.Name = "results";
            results.Size = new Size(780, 120);
            results.TabIndex = 5;
            results.ScrollBars = ScrollBars.Vertical;
            
            btnAddVar.Location = new Point(751, 12);
            btnAddVar.Name = "btnAddVar";
            btnAddVar.Size = new Size(171, 68);
            btnAddVar.TabIndex = 8;
            btnAddVar.Text = "Добавить переменную";
            btnAddVar.UseVisualStyleBackColor = true;
            btnAddVar.Click += btnAddVar_Click;
            
            btnSave.Location = new Point(798, 324);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(124, 120);
            btnSave.TabIndex = 9;
            btnSave.Text = "Сохранить в файл";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            
            btnLoad.Location = new Point(263, 49);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(250, 31);
            btnLoad.TabIndex = 10;
            btnLoad.Text = "Загрузить из файла";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            
            ClientSize = new Size(934, 480);
            Controls.Add(txtEqs);
            Controls.Add(txtVars);
            Controls.Add(btnGenSLAU);
            Controls.Add(txtConverg);
            Controls.Add(btnSolve);
            Controls.Add(btnAddEq);
            Controls.Add(results);
            Controls.Add(btnAddVar);
            Controls.Add(btnSave);
            Controls.Add(btnLoad);
            Name = "Form1";
            Text = "Решение СЛАУ методом Зейделя";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtEqs;
        private System.Windows.Forms.TextBox txtVars;
        private System.Windows.Forms.Button btnGenSLAU;
        private System.Windows.Forms.TextBox txtConverg;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnAddEq;
        private System.Windows.Forms.TextBox results;
        private System.Windows.Forms.Button btnAddVar;
        private System.Windows.Forms.Button btnSave;
    }
}
