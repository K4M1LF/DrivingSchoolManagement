using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DrivingSchoolManagement
{
    public partial class ScheduleDrivingForm : Form
    {
        private DateTimePicker dateInput;
        private ComboBox instructorComboBox;
        private ComboBox studentComboBox;
        private ComboBox vehicleComboBox;
        private Button scheduleButton;

        private AppManager appManager;

        public ScheduleDrivingForm()
        {
            appManager = AppManager.GetInstance();
            InitializeComponents();
            PopulateComboBoxes();
        }

        private void InitializeComponents()
        {
            this.Text = "Planowanie Jazdy Praktycznej";
            this.Size = new System.Drawing.Size(500, 300);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dateInput = new DateTimePicker();
            dateInput.Location = new System.Drawing.Point(150, 20);
            dateInput.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(dateInput);

            instructorComboBox = new ComboBox();
            instructorComboBox.Location = new System.Drawing.Point(150, 60);
            instructorComboBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(instructorComboBox);

            studentComboBox = new ComboBox();
            studentComboBox.Location = new System.Drawing.Point(150, 100);
            studentComboBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(studentComboBox);

            vehicleComboBox = new ComboBox();
            vehicleComboBox.Location = new System.Drawing.Point(150, 140);
            vehicleComboBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(vehicleComboBox);

            scheduleButton = new Button();
            scheduleButton.Location = new System.Drawing.Point(150, 180);
            scheduleButton.Size = new System.Drawing.Size(200, 30);
            scheduleButton.Text = "Zaplanuj Jazdę";
            scheduleButton.Click += ScheduleButton_Click;
            this.Controls.Add(scheduleButton);

            AddLabel("Data Jazdy:", 20, 20);
            AddLabel("Instruktor:", 20, 60);
            AddLabel("Kursant:", 20, 100);
            AddLabel("Pojazd:", 20, 140);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new System.Drawing.Point(x, y);
            label.AutoSize = true;
            this.Controls.Add(label);
        }

        private void PopulateComboBoxes()
        {
            foreach (var instructor in appManager.Instructors)
            {
                instructorComboBox.Items.Add($"{instructor.FirstName} {instructor.LastName}");
            }

            foreach (var student in appManager.Students)
            {
                studentComboBox.Items.Add($"{student.FirstName} {student.LastName}");
            }

            foreach (var vehicle in appManager.Vehicles)
            {
                vehicleComboBox.Items.Add($"{vehicle.Brand} {vehicle.Model}");
            }

            if (instructorComboBox.Items.Count > 0)
                instructorComboBox.SelectedIndex = 0;

            if (studentComboBox.Items.Count > 0)
                studentComboBox.SelectedIndex = 0;

            if (vehicleComboBox.Items.Count > 0)
                vehicleComboBox.SelectedIndex = 0;
        }

        private void ScheduleButton_Click(object sender, EventArgs e)
        {
            string selectedInstructor = instructorComboBox.SelectedItem.ToString();
            string selectedStudent = studentComboBox.SelectedItem.ToString();
            string selectedVehicle = vehicleComboBox.SelectedItem.ToString();
            DateTime selectedDate = dateInput.Value;

            Instructor instructor = FindInstructorByName(selectedInstructor);
            Student student = FindStudentByName(selectedStudent);
            Vehicle vehicle = FindVehicleByName(selectedVehicle);

            if (instructor != null && student != null && vehicle != null)
            {
                PracticalDriving newDriving = PracticalDriving.Create(0, student, instructor, vehicle, selectedDate);
                appManager.ScheduleDriving(newDriving);      
                this.Close();
            }
            else
            {
                MessageBox.Show("Błąd podczas pobierania danych. Upewnij się, że wybrałeś wszystkie opcje.");
            }
        }

        private Instructor FindInstructorByName(string name)
        {
            foreach (var instructor in appManager.Instructors)
            {
                if ($"{instructor.FirstName} {instructor.LastName}" == name)
                {
                    return instructor;
                }
            }
            return null;
        }

        private Student FindStudentByName(string name)
        {
            foreach (var student in appManager.Students)
            {
                if ($"{student.FirstName} {student.LastName}" == name)
                {
                    return student;
                }
            }
            return null;
        }

        private Vehicle FindVehicleByName(string name)
        {
            foreach (var vehicle in appManager.Vehicles)
            {
                if ($"{vehicle.Brand} {vehicle.Model}" == name)
                {
                    return vehicle;
                }
            }
            return null;
        }
    }
}
