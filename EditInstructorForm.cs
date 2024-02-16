using System;
using System.Windows.Forms;

namespace DrivingSchoolManagement
{
    public partial class EditInstructorForm : Form
    {
        private Label firstNameLabel;
        private TextBox firstNameTextBox;
        private Label lastNameLabel;
        private TextBox lastNameTextBox;
        private Label dateOfBirthLabel;
        private DateTimePicker dateOfBirthDateTimePicker;
        private Label phoneNumberLabel;
        private TextBox phoneNumberTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label salaryLabel;
        private TextBox salaryTextBox;
        private Button saveButton;

        private Instructor editedInstructor;
        private bool isEditing;

        public EditInstructorForm(Instructor instructor, bool isEditing)
        {
            InitializeComponents();
            this.isEditing = isEditing;
            if (isEditing)
            {
                editedInstructor = instructor;
                PopulateForm();
            }
            else
            {
                editedInstructor = new Instructor(0, "", "", DateTime.Now, "", "", 0);
            }
        }

        private void InitializeComponents()
        {
            this.firstNameLabel = new Label();
            this.firstNameTextBox = new TextBox();
            this.lastNameLabel = new Label();
            this.lastNameTextBox = new TextBox();
            this.dateOfBirthLabel = new Label();
            this.dateOfBirthDateTimePicker = new DateTimePicker();
            this.phoneNumberLabel = new Label();
            this.phoneNumberTextBox = new TextBox();
            this.emailLabel = new Label();
            this.emailTextBox = new TextBox();
            this.salaryLabel = new Label();
            this.salaryTextBox = new TextBox();
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

            this.dateOfBirthLabel.AutoSize = true;
            this.dateOfBirthLabel.Location = new System.Drawing.Point(12, 67);
            this.dateOfBirthLabel.Name = "dateOfBirthLabel";
            this.dateOfBirthLabel.Size = new System.Drawing.Size(87, 13);
            this.dateOfBirthLabel.TabIndex = 4;
            this.dateOfBirthLabel.Text = "Data urodzenia:";

            this.dateOfBirthDateTimePicker.Location = new System.Drawing.Point(120, 64);
            this.dateOfBirthDateTimePicker.Name = "dateOfBirthDateTimePicker";
            this.dateOfBirthDateTimePicker.Size = new System.Drawing.Size(150, 20);
            this.dateOfBirthDateTimePicker.TabIndex = 5;

            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Location = new System.Drawing.Point(12, 93);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Size = new System.Drawing.Size(87, 13);
            this.phoneNumberLabel.TabIndex = 6;
            this.phoneNumberLabel.Text = "Numer telefonu:";

            this.phoneNumberTextBox.Location = new System.Drawing.Point(120, 90);
            this.phoneNumberTextBox.Name = "phoneNumberTextBox";
            this.phoneNumberTextBox.Size = new System.Drawing.Size(150, 20);
            this.phoneNumberTextBox.TabIndex = 7;

            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(12, 119);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(38, 13);
            this.emailLabel.TabIndex = 8;
            this.emailLabel.Text = "Email:";

            this.emailTextBox.Location = new System.Drawing.Point(120, 116);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(150, 20);
            this.emailTextBox.TabIndex = 9;

            this.salaryLabel.AutoSize = true;
            this.salaryLabel.Location = new System.Drawing.Point(12, 145);
            this.salaryLabel.Name = "salaryLabel";
            this.salaryLabel.Size = new System.Drawing.Size(70, 13);
            this.salaryLabel.TabIndex = 10;
            this.salaryLabel.Text = "Wynagrodzenie:";

            this.salaryTextBox.Location = new System.Drawing.Point(120, 142);
            this.salaryTextBox.Name = "salaryTextBox";
            this.salaryTextBox.Size = new System.Drawing.Size(150, 20);
            this.salaryTextBox.TabIndex = 11;

            this.saveButton.Location = new System.Drawing.Point(120, 180);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            this.ClientSize = new System.Drawing.Size(300, 220);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.salaryTextBox);
            this.Controls.Add(this.salaryLabel);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.phoneNumberTextBox);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.dateOfBirthDateTimePicker);
            this.Controls.Add(this.dateOfBirthLabel);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.firstNameLabel);
            this.Name = "EditInstructorForm";
            this.Text = "Edytuj/Dodaj Instruktora";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PopulateForm()
        {
            firstNameTextBox.Text = editedInstructor.FirstName;
            lastNameTextBox.Text = editedInstructor.LastName;
            dateOfBirthDateTimePicker.Value = editedInstructor.DateOfBirth;
            phoneNumberTextBox.Text = editedInstructor.PhoneNumber;
            emailTextBox.Text = editedInstructor.Email;
            salaryTextBox.Text = editedInstructor.Salary.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            editedInstructor.FirstName = firstNameTextBox.Text;
            editedInstructor.LastName = lastNameTextBox.Text;
            editedInstructor.DateOfBirth = dateOfBirthDateTimePicker.Value;
            editedInstructor.PhoneNumber = phoneNumberTextBox.Text;
            editedInstructor.Email = emailTextBox.Text;
            int salary;
            if (int.TryParse(salaryTextBox.Text, out salary))
            {
                editedInstructor.Salary = salary;

                if (isEditing)
                {
                   AppManager.GetInstance().UpdateInstructor(editedInstructor);
                }
                else
                {
                    AppManager.GetInstance().AddInstructor(editedInstructor);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawne wynagrodzenie.");
            }
        }
    }
}
