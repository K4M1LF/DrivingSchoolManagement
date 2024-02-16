using System;
using System.Windows.Forms;

namespace DrivingSchoolManagement
{
    public partial class EditVehicleForm : Form
    {
        private Label brandLabel;
        private TextBox brandTextBox;
        private Label modelLabel;
        private TextBox modelTextBox;
        private Label yearLabel;
        private NumericUpDown yearNumericUpDown;
        private Label priceLabel;
        private NumericUpDown priceNumericUpDown;
        private Label drivingLicenseCategoryLabel;
        private TextBox drivingLicenseCategoryTextBox;
        private Label conditionLabel;
        private ComboBox conditionComboBox;
        private Button saveButton;

        private Vehicle editedVehicle;
        private bool isEditing;

        public EditVehicleForm(Vehicle vehicle, bool isEditing)
        {
            InitializeComponents();
            this.isEditing = isEditing;
            if (isEditing)
            {
                editedVehicle = vehicle;
                PopulateForm();
            }
            else
            {
                editedVehicle = new Vehicle(0, "", "", 1980, 0, TechnicalCondition.Bad, "B");
            }
        }

        private void InitializeComponents()
        {
            this.brandLabel = new Label();
            this.brandTextBox = new TextBox();
            this.modelLabel = new Label();
            this.modelTextBox = new TextBox();
            this.yearLabel = new Label();
            this.yearNumericUpDown = new NumericUpDown();
            this.priceLabel = new Label();
            this.priceNumericUpDown = new NumericUpDown();
            this.drivingLicenseCategoryLabel = new Label();
            this.drivingLicenseCategoryTextBox = new TextBox();
            this.conditionLabel = new Label();
            this.conditionComboBox = new ComboBox();
            this.saveButton = new Button();

            this.SuspendLayout();

            this.brandLabel.AutoSize = true;
            this.brandLabel.Location = new System.Drawing.Point(12, 15);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.Size = new System.Drawing.Size(45, 13);
            this.brandLabel.TabIndex = 0;
            this.brandLabel.Text = "Marka:";

            this.brandTextBox.Location = new System.Drawing.Point(120, 12);
            this.brandTextBox.Name = "brandTextBox";
            this.brandTextBox.Size = new System.Drawing.Size(150, 20);
            this.brandTextBox.TabIndex = 1;

            this.modelLabel.AutoSize = true;
            this.modelLabel.Location = new System.Drawing.Point(12, 41);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(42, 13);
            this.modelLabel.TabIndex = 2;
            this.modelLabel.Text = "Model:";

            this.modelTextBox.Location = new System.Drawing.Point(120, 38);
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(150, 20);
            this.modelTextBox.TabIndex = 3;

            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(12, 67);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(29, 13);
            this.yearLabel.TabIndex = 4;
            this.yearLabel.Text = "Rok:";

            this.yearNumericUpDown.Location = new System.Drawing.Point(120, 64);
            this.yearNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.yearNumericUpDown.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.yearNumericUpDown.Name = "yearNumericUpDown";
            this.yearNumericUpDown.Size = new System.Drawing.Size(150, 20);
            this.yearNumericUpDown.TabIndex = 5;
            this.yearNumericUpDown.Value = new decimal(new int[] {
            2022,
            0,
            0,
            0});

            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(12, 93);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(35, 13);
            this.priceLabel.TabIndex = 6;
            this.priceLabel.Text = "Cena:";

            this.priceNumericUpDown.Location = new System.Drawing.Point(120, 90);
            this.priceNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.priceNumericUpDown.Name = "priceNumericUpDown";
            this.priceNumericUpDown.Size = new System.Drawing.Size(150, 20);
            this.priceNumericUpDown.TabIndex = 7;

            this.drivingLicenseCategoryLabel.AutoSize = true;
            this.drivingLicenseCategoryLabel.Location = new System.Drawing.Point(12, 119);
            this.drivingLicenseCategoryLabel.Name = "drivingLicenseCategoryLabel";
            this.drivingLicenseCategoryLabel.Size = new System.Drawing.Size(96, 13);
            this.drivingLicenseCategoryLabel.TabIndex = 8;
            this.drivingLicenseCategoryLabel.Text = "Kategoria:";

            this.drivingLicenseCategoryTextBox.Location = new System.Drawing.Point(120, 116);
            this.drivingLicenseCategoryTextBox.Name = "drivingLicenseCategoryTextBox";
            this.drivingLicenseCategoryTextBox.Size = new System.Drawing.Size(150, 20);
            this.drivingLicenseCategoryTextBox.TabIndex = 9;

            this.conditionLabel.AutoSize = true;
            this.conditionLabel.Location = new System.Drawing.Point(12, 145);
            this.conditionLabel.Name = "conditionLabel";
            this.conditionLabel.Size = new System.Drawing.Size(85, 13);
            this.conditionLabel.TabIndex = 10;
            this.conditionLabel.Text = "Stan techniczny:";

            this.conditionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Items.AddRange(new object[] {
            TechnicalCondition.VeryGood,
            TechnicalCondition.Good,
            TechnicalCondition.Average,
            TechnicalCondition.Bad});
            this.conditionComboBox.Location = new System.Drawing.Point(120, 142);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(150, 21);
            this.conditionComboBox.TabIndex = 11;

            this.saveButton.Location = new System.Drawing.Point(120, 180);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            this.ClientSize = new System.Drawing.Size(300, 220);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.conditionLabel);
            this.Controls.Add(this.drivingLicenseCategoryTextBox);
            this.Controls.Add(this.drivingLicenseCategoryLabel);
            this.Controls.Add(this.priceNumericUpDown);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.yearNumericUpDown);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.modelTextBox);
            this.Controls.Add(this.modelLabel);
            this.Controls.Add(this.brandTextBox);
            this.Controls.Add(this.brandLabel);
            this.Name = "EditVehicleForm";
            this.Text = "Edytuj/Dodaj Pojazd";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PopulateForm()
        {
            brandTextBox.Text = editedVehicle.Brand;
            modelTextBox.Text = editedVehicle.Model;
            yearNumericUpDown.Value = editedVehicle.Year;
            priceNumericUpDown.Value = (decimal)editedVehicle.Price;
            drivingLicenseCategoryTextBox.Text = editedVehicle.DrivingLicenseCategory;
            conditionComboBox.SelectedItem = editedVehicle.Condition;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            editedVehicle.Brand = brandTextBox.Text;
            editedVehicle.Model = modelTextBox.Text;
            editedVehicle.Year = (int)yearNumericUpDown.Value;
            editedVehicle.Price = (double)priceNumericUpDown.Value;
            editedVehicle.DrivingLicenseCategory = drivingLicenseCategoryTextBox.Text;
            editedVehicle.Condition = (TechnicalCondition)conditionComboBox.SelectedItem;

            if (isEditing)
            {
                AppManager.GetInstance().UpdateVehicle(editedVehicle);                
            }
            else
            {
                AppManager.GetInstance().AddVehicle(editedVehicle);               
            }
            this.Close();

            
        }
    }
}
