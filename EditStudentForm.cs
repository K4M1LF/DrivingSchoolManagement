using System;
using System.Windows.Forms;

namespace DrivingSchoolManagement
{
    public partial class EditStudentForm : Form
    {
        private Label firstNameLabel;
        private TextBox firstNameTextBox;
        private Label lastNameLabel;
        private TextBox lastNameTextBox;
        private Label phoneNumberLabel;
        private TextBox phoneNumberTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label drivingLicenseCategoryLabel;
        private TextBox drivingLicenseCategoryTextBox;
        private Button saveButton;

        private Student editedStudent;
        private bool isEditing;

        public EditStudentForm(Student student, bool isEditing)
        {
            InitializeComponents();
            this.isEditing = isEditing;
            if (isEditing)
            {
                editedStudent = student;
                PopulateForm();
            }
            else
            {
                editedStudent = new Student(0, "", "", "", "", "");
            }

        }
        private void InitializeComponents()
        {
            this.firstNameLabel = new Label();
            this.firstNameTextBox = new TextBox();
            this.lastNameLabel = new Label();
            this.lastNameTextBox = new TextBox();
            this.phoneNumberLabel = new Label();
            this.phoneNumberTextBox = new TextBox();
            this.emailLabel = new Label();
            this.emailTextBox = new TextBox();
            this.drivingLicenseCategoryLabel = new Label();
            this.drivingLicenseCategoryTextBox = new TextBox();
            this.saveButton = new Button();

            this.SuspendLayout();

            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(12, 15);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(50, 13);
            this.firstNameLabel.TabIndex = 0;
            this.firstNameLabel.Text = "Imię:";

            this.firstNameTextBox.Location = new System.Drawing.Point(120, 12);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.firstNameTextBox.TabIndex = 1;

            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(12, 41);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(70, 13);
            this.lastNameLabel.TabIndex = 2;
            this.lastNameLabel.Text = "Nazwisko:";

            this.lastNameTextBox.Location = new System.Drawing.Point(120, 38);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.lastNameTextBox.TabIndex = 3;

            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Location = new System.Drawing.Point(12, 67);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Size = new System.Drawing.Size(87, 13);
            this.phoneNumberLabel.TabIndex = 4;
            this.phoneNumberLabel.Text = "Numer telefonu:";

            this.phoneNumberTextBox.Location = new System.Drawing.Point(120, 64);
            this.phoneNumberTextBox.Name = "phoneNumberTextBox";
            this.phoneNumberTextBox.Size = new System.Drawing.Size(150, 20);
            this.phoneNumberTextBox.TabIndex = 5;

            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(12, 93);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(38, 13);
            this.emailLabel.TabIndex = 6;
            this.emailLabel.Text = "Email:";

            this.emailTextBox.Location = new System.Drawing.Point(120, 90);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(150, 20);
            this.emailTextBox.TabIndex = 7;

            this.drivingLicenseCategoryLabel.AutoSize = true;
            this.drivingLicenseCategoryLabel.Location = new System.Drawing.Point(12, 119);
            this.drivingLicenseCategoryLabel.Name = "drivingLicenseCategoryLabel";
            this.drivingLicenseCategoryLabel.Size = new System.Drawing.Size(105, 13);
            this.drivingLicenseCategoryLabel.TabIndex = 8;
            this.drivingLicenseCategoryLabel.Text = "Kategoria:";

            this.drivingLicenseCategoryTextBox.Location = new System.Drawing.Point(120, 116);
            this.drivingLicenseCategoryTextBox.Name = "drivingLicenseCategoryTextBox";
            this.drivingLicenseCategoryTextBox.Size = new System.Drawing.Size(150, 20);
            this.drivingLicenseCategoryTextBox.TabIndex = 9;

            this.saveButton.Location = new System.Drawing.Point(120, 150);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.drivingLicenseCategoryTextBox);
            this.Controls.Add(this.drivingLicenseCategoryLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.phoneNumberTextBox);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.firstNameLabel);
            this.Name = "EditStudentForm";
            this.Text = "Edytuj/Dodaj Kursanta";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PopulateForm()
        {
            firstNameTextBox.Text = editedStudent.FirstName;
            lastNameTextBox.Text = editedStudent.LastName;
            phoneNumberTextBox.Text = editedStudent.PhoneNumber;
            emailTextBox.Text = editedStudent.Email;
            drivingLicenseCategoryTextBox.Text = editedStudent.DrivingLicenseCategory;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            editedStudent.FirstName = firstNameTextBox.Text;
            editedStudent.LastName = lastNameTextBox.Text;
            editedStudent.PhoneNumber = phoneNumberTextBox.Text;
            editedStudent.Email = emailTextBox.Text;
            editedStudent.DrivingLicenseCategory = drivingLicenseCategoryTextBox.Text;

            if (isEditing)
            {
                AppManager.GetInstance().UpdateStudent(editedStudent);
            }
            else
            {
                AppManager.GetInstance().AddStudent(editedStudent);
            }
            this.Close();
        }
    }
}
