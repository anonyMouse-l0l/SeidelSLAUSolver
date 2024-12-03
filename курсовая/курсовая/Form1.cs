namespace курсовая
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int windowWidth = this.Width - this.ClientSize.Width;
            int windowHeight = this.Height - this.ClientSize.Height;
            int minWidth = 934 + windowWidth;
            int minHeight = 480 + windowHeight;
            this.MinimumSize = new Size(minWidth, minHeight);
        }
        private List<TextBox> KTextBoxes = new List<TextBox>();
        private List<TextBox> constTextBoxes = new List<TextBox>();

        private void btnGenSLAU_Click(object sender, EventArgs e)
        {
            try
            {
                ClrCtrls();
                int eqs = int.Parse(txtEqs.Text);
                int vars = int.Parse(txtVars.Text);
                if (eqs <= 0 || vars <= 0)
                    throw new Exception("Количество уравнений и неизвестных должно быть больше нуля.");
                int startX = 10, startY = 100;
                int textBoxWidth = 50, labelWidth = 30, plusWidth = 20, buttonWidth = 30;
                int spacingX = 25, spacingY = 20;
                int controlHeight = 30;
                int formHeight = startY + eqs * (controlHeight + spacingY) + 100;
                int formWidth = startX + vars * (textBoxWidth + labelWidth + plusWidth + spacingX) + buttonWidth + 100;
                for (int i = 0; i < eqs; i++)
                {
                    for (int j = 0; j < vars; j++)
                    {
                        TextBox txtK = new TextBox
                        {
                            Width = textBoxWidth,
                            Location = new Point(startX + j * (textBoxWidth + labelWidth + plusWidth + spacingX), startY + i * (controlHeight + spacingY))
                        };
                        KTextBoxes.Add(txtK);
                        Controls.Add(txtK);
                        txtK.BringToFront();
                        Label lblVar = new Label
                        {
                            Text = $"x{j + 1}",
                            AutoSize = true,
                            Location = new Point(txtK.Right + 5, txtK.Top + (controlHeight - 15) / 2)
                        };
                        Controls.Add(lblVar);
                        if (j < vars - 1)
                        {
                            Label lblPlus = new Label
                            {
                                Text = "+",
                                AutoSize = true,
                                Location = new Point(lblVar.Right + 10, txtK.Top + (controlHeight - 15) / 2)
                            };
                            Controls.Add(lblPlus);
                        }
                    }
                    TextBox txtConst = new TextBox
                    {
                        Width = textBoxWidth,
                        Location = new Point(startX + vars * (textBoxWidth + labelWidth + plusWidth + spacingX), startY + i * (controlHeight + spacingY))
                    };
                    constTextBoxes.Add(txtConst);
                    Controls.Add(txtConst);

                    Label lblEquals = new Label
                    {
                        Text = "=",
                        AutoSize = true,
                        Location = new Point(txtConst.Left - 25, txtConst.Top + (controlHeight - 15) / 2)
                    };
                    Controls.Add(lblEquals);
                }
                results.Text = string.Empty;
                results.Top = startY + eqs * (controlHeight + spacingY) + 20;
                btnSave.Location = new Point(results.Right + 10, results.Top);
                this.ClientSize = new Size(formWidth, formHeight + 60);
                this.AutoScroll = true;
                this.PerformLayout();
                this.Refresh();
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Ошибка при генерации системы уравнений");
            }
        }

        private void btnAddEq_Click(object sender, EventArgs e)
        {
            try
            {
                int eqs = constTextBoxes.Count;
                int vars = KTextBoxes.Count / eqs;
                AddMissingEqs(eqs, vars);
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Сначала добавьте уравнения");
            }
        }
        private void btnAddVar_Click(object sender, EventArgs e)
        {
            try
            {
                int currentVars = KTextBoxes.Count / constTextBoxes.Count;
                int newVarIndex = currentVars + 1;
                int eqs = constTextBoxes.Count;
                int textBoxWidth = 50, labelWidth = 30, plusWidth = 20;
                int spacingX = 25;
                int controlHeight = 30;
                for (int i = 0; i < eqs; i++)
                {
                    TextBox txtConst = constTextBoxes[i];
                    txtConst.Location = new Point(
                        txtConst.Location.X + textBoxWidth + labelWidth + plusWidth + spacingX,
                        txtConst.Location.Y
                    );
                    TextBox txtK = new TextBox
                    {
                        Width = textBoxWidth,
                        Location = new Point(txtConst.Location.X - (textBoxWidth + labelWidth + plusWidth + spacingX), txtConst.Location.Y)
                    };
                    KTextBoxes.Add(txtK);
                    Controls.Add(txtK);
                    txtK.BringToFront();
                    Label lblVar = new Label
                    {
                        Text = $"x{newVarIndex}",
                        AutoSize = true,
                        Location = new Point(txtK.Right + 5, txtK.Top + (controlHeight - 15) / 2)
                    };
                    Controls.Add(lblVar);
                    lblVar.BringToFront();
                    Label lblEqual = new Label
                    {
                        Text = "=",
                        AutoSize = true,
                        Location = new Point(lblVar.Right + 10, lblVar.Top)

                    };
                    Controls.Add(lblEqual);
                    Label lblPlus = new Label
                    {
                        Text = "+",
                        AutoSize = true,
                        Location = new Point(lblVar.Left - 83, lblVar.Top)
                    };
                    Controls.Add(lblPlus);
                    lblPlus.BringToFront();
                    
                }
                newVarIndex++;
                ReindexKTextBoxes();
                CorrectFormSize();
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Ошибка при добавлении неизвестного");
            }
        }
        private void ReindexKTextBoxes()
        {
            KTextBoxes = KTextBoxes
                .OrderBy(tb => tb.Location.Y)
                .ThenBy(tb => tb.Location.X)
                .ToList();
        }

        private void AddMissingEqs(int eqs, int vars)
        {
            try
            {
                int missingEquations = vars - eqs;
                for (int i = 0; i < missingEquations; i++)
                {
                    int currentEquation = eqs + i;
                    int startX = 10, startY = 100;
                    int textBoxWidth = 50, labelWidth = 30, plusWidth = 20;
                    int spacingX = 25, spacingY = 20;
                    int controlHeight = 30;
                    for (int j = 0; j < vars; j++)
                    {
                        TextBox txtK = new TextBox
                        {
                            Width = textBoxWidth,
                            Location = new Point(startX + j * (textBoxWidth + labelWidth + plusWidth + spacingX), startY + currentEquation * (controlHeight + spacingY))
                        };
                        KTextBoxes.Add(txtK);
                        Controls.Add(txtK);
                        Label lblVar = new Label
                        {
                            Text = $"x{j + 1}",
                            AutoSize = true,
                            Location = new Point(txtK.Right + 5, txtK.Top + (controlHeight - 15) / 2)
                        };
                        Controls.Add(lblVar);
                        if (j < vars - 1)
                        {
                            Label lblPlus = new Label
                            {
                                Text = "+",
                                AutoSize = true,
                                Location = new Point(lblVar.Right + 10, txtK.Top + (controlHeight - 15) / 2)
                            };
                            Controls.Add(lblPlus);
                        }
                    }
                    TextBox txtConst = new TextBox
                    {
                        Width = textBoxWidth,
                        Location = new Point(startX + vars * (textBoxWidth + labelWidth + plusWidth + spacingX), startY + currentEquation * (controlHeight + spacingY))
                    };
                    constTextBoxes.Add(txtConst);
                    Controls.Add(txtConst);

                    Label lblEquals = new Label
                    {
                        Text = "=",
                        AutoSize = true,
                        Location = new Point(txtConst.Left - 25, txtConst.Top + (controlHeight - 15) / 2)
                    };
                    Controls.Add(lblEquals);
                }
                if ((KTextBoxes.Count / constTextBoxes.Count) >= constTextBoxes.Count)
                {
                    results.Top = 100 + (KTextBoxes.Count / constTextBoxes.Count) * (30 + 20) + 20;
                    btnSave.Location = new Point(results.Right + 10, results.Top);
                    CorrectFormSize();
                }
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Ошибка при добавлении недостающих уравнений");
            }
        }
        private void RmExtraEqs(int eqs, int vars)
        {
            try
            {
                int extraEquations = eqs - vars;
                for (int i = 0; i < extraEquations; i++)
                {
                    for (int j = 0; j < vars; j++)
                    {
                        var control = KTextBoxes.Last();
                        Controls.Remove(control);
                        KTextBoxes.Remove(control);
                    }
                    var constControl = constTextBoxes.Last();
                    Controls.Remove(constControl);
                    constTextBoxes.Remove(constControl);
                    var lblEqual = this.Controls.OfType<Label>().Where(lbl => lbl.Text == "=").ToList();
                    Controls.Remove(lblEqual.Last());
                    var lblX = this.Controls.OfType<Label>().Where(lbl => lbl.Text.StartsWith("x")).ToList();
                    for (int j = 0; j < vars; j++)
                    {
                        var xs = lblX.Last();
                        Controls.Remove(xs);
                        lblX.Remove(xs);
                    }
                    var lblPluses = this.Controls.OfType<Label>().Where(lbl => lbl.Text == "+").ToList();

                    for (int j = 0; j < vars - 1; j++)
                    {
                        var plus = lblPluses.Last();
                        Controls.Remove(plus);
                        lblPluses.Remove(plus);
                    }
                }
                results.Top = 80 + (KTextBoxes.Count / constTextBoxes.Count) * (30 + 20) + 20;
                btnSave.Location = new Point(results.Right + 10, results.Top);
                CorrectFormSize();
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Ошибка при удалении лишних уравнений");
            }
        }

        private void CorrectFormSize()
        {
            int maxWidth = 0;
            int maxHeight = 0;
            foreach (Control control in this.Controls)
            {
                int controlRight = control.Right;
                int controlBottom = control.Bottom;
                if (controlRight > maxWidth)
                    maxWidth = controlRight;
                if (controlBottom > maxHeight)
                    maxHeight = controlBottom;
            }
            int paddingWidth = 30; 
            int paddingHeight = 50; 
            int calculatedWidth = maxWidth + paddingWidth;
            int calculatedHeight = maxHeight + paddingHeight;
            this.ClientSize = new Size(
                Math.Max(calculatedWidth, this.MinimumSize.Width - (this.Width - this.ClientSize.Width)),
                Math.Max(calculatedHeight, this.MinimumSize.Height - (this.Height - this.ClientSize.Height))
            );
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            try
            {
                int eqs = constTextBoxes.Count;
                int vars = KTextBoxes.Count / eqs;
                if (eqs != vars)
                {
                    string message = eqs > vars
                        ? "Количество уравнений больше количества неизвестных. Убрать лишние уравнения?"
                        : "Количество уравнений меньше количества неизвестных. Добавить недостающие уравнения?";
                    DialogResult resul = MessageBox.Show(
                        message,
                        "Неквадратная матрица",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning
                    );
                    if (resul == DialogResult.Yes)
                    {
                        if (eqs > vars)
                        {
                            RmExtraEqs(eqs, vars);
                        }
                        else
                        {
                            AddMissingEqs(eqs, vars);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                var matrix = GetMatrix();
                var consts = GetConsts();
                double converg = double.Parse(txtConverg.Text);
                var result = Solver.Solve(matrix, consts, converg);
                results.Text = string.Join(Environment.NewLine, result.Select((x, i) => $"x{i + 1} = {x:F5}"));
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Введите корректные данные");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                    saveFileDialog.Title = "Сохранить систему уравнений и результаты";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            int eqs = constTextBoxes.Count; 
                            int vars = KTextBoxes.Count / eqs; 
                            writer.WriteLine("Уравнения:");
                            for (int i = 0; i < eqs; i++)
                            {
                                for (int j = 0; j < vars; j++)
                                {
                                    writer.Write(KTextBoxes[i * vars + j].Text + $"x{j + 1}");
                                    if (j < vars - 1)
                                        writer.Write(" + ");
                                }
                                writer.Write(" = " + constTextBoxes[i].Text);
                                writer.WriteLine();
                            }
                            writer.WriteLine($"\nТочность: {txtConverg.Text}");
                            writer.WriteLine($"\r\nРезультат:\r\n{results.Text}");
                        }
                        MessageBox.Show("Данные успешно сохранены!");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show("Ошибка при сохранении файла. :(");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    openFileDialog.Title = "Открыть файл с системой уравнений";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] lines = File.ReadAllLines(openFileDialog.FileName);
                        int eqs = constTextBoxes.Count;
                        int vars = KTextBoxes.Count / eqs;
                        if (lines == null)
                            throw new Exception("Файл пуст");
                        if (lines.Length != eqs)
                            throw new Exception($"Число строк в файле ({lines.Length}) не соответствует числу уравнений ({eqs}).");
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] parts = lines[i].Split(';');
                            if (parts.Length != vars + 1)
                                throw new Exception($"Ошибка в строке {i + 1}: ожидается {vars + 1} неизвестных, а в файле-то {parts.Length}. ;)");
                            for (int j = 0; j < vars; j++)
                            {
                                KTextBoxes[i * vars + j].Text = parts[j];
                            }
                            constTextBoxes[i].Text = parts[vars];
                        }
                        MessageBox.Show("Данные успешно загружены!");
                    }
                }
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Файл пуст или не создана система уравнений");
            }
            catch (Exception ex)
            {
                ErrLogger(ex);
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void ClrCtrls()
        {
            foreach (var control in KTextBoxes.Concat(constTextBoxes))
            {
                this.Controls.Remove(control);
            }
            var labels = this.Controls.OfType<Label>().Where(lbl => lbl.Text.StartsWith("x") || lbl.Text == "=").ToList();
            foreach (var label in labels)
            {
                this.Controls.Remove(label);
            }
            var plusLabels = this.Controls.OfType<Label>().Where(lbl => lbl.Text == "+").ToList();
            foreach (var label in plusLabels)
            {
                this.Controls.Remove(label);
            }
            KTextBoxes.Clear();
            constTextBoxes.Clear();
        }

        private double[,] GetMatrix()
        {
            int cols = KTextBoxes.Count / constTextBoxes.Count; 
            int rows = constTextBoxes.Count;
            double[,] matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int index = i * cols + j;
                    matrix[i, j] = double.Parse(KTextBoxes[index].Text);
                }
            }
            return matrix;
        }

        private double[] GetConsts()
        {
            int size = constTextBoxes.Count;
            double[] consts = new double[size];

            for (int i = 0; i < size; i++)
            {
                consts[i] = double.Parse(constTextBoxes[i].Text);
            }
            return consts;
        }
        private void ErrLogger(Exception ex)
        {
            string logFilePath = Path.Combine(Application.StartupPath, "error_log.txt");
            string logMessage = $"[{DateTime.Now}] Исключение: {ex.Message}\n Трассировка стека: {ex.StackTrace}\n";
            try
            {
                File.AppendAllText(logFilePath, logMessage);
            }
            catch
            {
                MessageBox.Show("Не удалось записать ошибку в лог-файл");
            }
        }
    }

    public static class Solver
    {
        public static double[] Solve(double[,] matrix, double[] consts, double converg, int maxIterations = 1000)
        {
            int size = consts.Length;
            double[] x = new double[size];
            double[] prevX = new double[size];
            double norm;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                for (int i = 0; i < size; i++)
                {
                    double sum = consts[i];
                    for (int j = 0; j < size; j++)
                    {
                        if (j != i)
                            sum -= matrix[i, j] * x[j];
                    }
                    x[i] = sum / matrix[i, i];
                }
                norm = 0;
                for (int i = 0; i < size; i++)
                    norm += Math.Abs(x[i] - prevX[i]);

                if (norm < converg)
                    return x;

                Array.Copy(x, prevX, size);
            }
            throw new Exception("Метод не сходится за заданное число итераций.");
        }
    }
}
