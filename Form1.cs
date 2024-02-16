using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace DrivingSchoolManagement
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView;
        private Button displayStudentsButton;
        private Button displayInstructorsButton;
        private Button displayVehiclesButton;
        private ContextMenuStrip contextMenuStrip;

        private Button saveToCsvButton;

        private int currentViewId;
        private AppManager appManager;
        private int selectedRowIndex;
      
        public Form1()
        {
            appManager = AppManager.GetInstance();
            InitializeComponent();
            InitializeComponents();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            appManager.DataUpdated += AppManager_DataUpdated;
        }

        private void AppManager_DataUpdated(object sender, EventArgs e)
        {
            UpdateViews();
        }

        private void UpdateViews()
        {
            if (currentViewId == 0)
            {
                DisplayStudents();
            }
            else if(currentViewId == 1)
            {
                DisplayInstructors();

            }else if(currentViewId==2)
            {
                DisplayVehicles();
            }       
        }

        private void InitializeComponents()
        {
            currentViewId = 0;
            this.Size = new Size(800, 600);
            this.Text = "Ośrodek Szkolenia Kierowców";

            dataGridView = new DataGridView();
            dataGridView.Size = new Size(700, 450);
            dataGridView.Location = new Point(40, 30);
            this.Controls.Add(dataGridView);

            displayStudentsButton = new Button();
            displayStudentsButton.Size = new Size(120, 30);
            displayStudentsButton.Location = new Point(50, 500);
            displayStudentsButton.Text = "Kursanci";
            displayStudentsButton.Click += displayStudentsButton_Click;
            this.Controls.Add(displayStudentsButton);

            displayInstructorsButton = new Button();
            displayInstructorsButton.Size = new Size(120, 30);
            displayInstructorsButton.Location = new Point(250, 500);
            displayInstructorsButton.Text = "Instruktorzy";
            displayInstructorsButton.Click += displayInstructorsButton_Click;
            this.Controls.Add(displayInstructorsButton);

            displayVehiclesButton = new Button();
            displayVehiclesButton.Size = new Size(120, 30);
            displayVehiclesButton.Location = new Point(450, 500);
            displayVehiclesButton.Text = "Pojazdy";
            displayVehiclesButton.Click += displayVehiclesButton_Click;
            this.Controls.Add(displayVehiclesButton);

            contextMenuStrip = new ContextMenuStrip();
            dataGridView.CellMouseClick += DataGridView_CellMouseClick;


            saveToCsvButton = new Button();
            saveToCsvButton.Size = new Size(120, 30);
            saveToCsvButton.Location = new Point(650, 500);
            saveToCsvButton.Text = "Zapisz do CSV";
            saveToCsvButton.Click += SaveToCsvButton_Click; // Dodaj obsługę zdarzenia
            this.Controls.Add(saveToCsvButton);

        }
        private void displayStudentsButton_Click(object sender, EventArgs e)
        {
            DisplayStudents();
            appManager.Schedule.PracticalDrivingSchedule = appManager.databaseManager.GetPracticalDrivingsFromDatabase();
        }

        private void displayInstructorsButton_Click(object sender, EventArgs e)
        {
            DisplayInstructors();
            appManager.Schedule.PracticalDrivingSchedule = appManager.databaseManager.GetPracticalDrivingsFromDatabase();
        }

        private void displayVehiclesButton_Click(object sender, EventArgs e)
        {
            DisplayVehicles();
        }

        private void DisplayStudents()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("Id", "ID");
            dataGridView.Columns.Add("FirstName", "Imię");
            dataGridView.Columns.Add("LastName", "Nazwisko");
            dataGridView.Columns.Add("PhoneNumber", "Numer telefonu");
            dataGridView.Columns.Add("Email", "Email");
            dataGridView.Columns.Add("DrivingLicenseCategory", "Kategoria prawa jazdy");

            foreach (var student in appManager.Students)
            {
             
                    dataGridView.Rows.Add(student.Id, student.FirstName, student.LastName, student.PhoneNumber,
                        student.Email, student.DrivingLicenseCategory);
                
            }

            dataGridView.Columns["Id"].Width = 50;
            dataGridView.Columns["DrivingLicenseCategory"].Width = 137;
            dataGridView.Columns["Email"].Width = 170; 
            currentViewId = 0;
        }

        private void DisplayInstructors()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("Id", "ID");
            dataGridView.Columns.Add("FirstName", "Imię");
            dataGridView.Columns.Add("LastName", "Nazwisko");
            dataGridView.Columns.Add("DateOfBirth", "Data urodzenia");
            dataGridView.Columns.Add("PhoneNumber", "Numer telefonu");
            dataGridView.Columns.Add("Email", "Email");
            dataGridView.Columns.Add("Salary", "Wynagrodzenie");

            dataGridView.Columns["DateOfBirth"].DefaultCellStyle.Format = "yyyy-MM-dd";

            foreach (var instructor in appManager.Instructors)
            {
                dataGridView.Rows.Add(instructor.Id, instructor.FirstName, instructor.LastName,
                    instructor.DateOfBirth, instructor.PhoneNumber, instructor.Email, instructor.Salary);
            }
            dataGridView.Columns["Id"].Width = 30; 
            dataGridView.Columns["Email"].Width = 140;
            dataGridView.Columns["PhoneNumber"].Width = 87;
            currentViewId = 1;
        }

        private void DisplayVehicles()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            dataGridView.Columns.Add("Id", "ID");
            dataGridView.Columns.Add("Brand", "Marka");
            dataGridView.Columns.Add("Model", "Model");
            dataGridView.Columns.Add("Year", "Rok produkcji");
            dataGridView.Columns.Add("Price", "Cena");
            dataGridView.Columns.Add("Condition", "Stan techniczny");
            dataGridView.Columns.Add("DrivingLicenseCategory", "Kategoria prawa jazdy");

            foreach (var vehicle in appManager.Vehicles)
            {
                dataGridView.Rows.Add(vehicle.Id, vehicle.Brand, vehicle.Model, vehicle.Year,
                    vehicle.Price, vehicle.Condition, vehicle.DrivingLicenseCategory);
            }
            dataGridView.Columns["Id"].Width = 30;
            dataGridView.Columns["Condition"].Width = 127;
            currentViewId = 2;
        }



        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView.Rows[e.RowIndex].Selected = true;

                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;

                contextMenuStrip.Items.Clear();
                contextMenuStrip.Items.Add("Dodaj", null, ContextMenuOption1_Click);
                contextMenuStrip.Items.Add("Edytuj", null, ContextMenuOption2_Click);
                contextMenuStrip.Items.Add("Usuń", null, ContextMenuOption3_Click);
                if (currentViewId == 2)
                {
                    contextMenuStrip.Items.Add("Incydenty", null, ContextMenuOption4_Click);
                }
                else
                {
                    contextMenuStrip.Items.Add("Harmonogram", null, ContextMenuOption4_Click);
                }
                contextMenuStrip.Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
            }
        }

        private void ContextMenuOption1_Click(object sender, EventArgs e)
        {
            if (currentViewId == 0)
            {
                OpenEditStudentForm(false);
            }
            else if (currentViewId == 1)
            {
                OpenInstructorForm(false);
            }else if(currentViewId == 2)
            {
                OpenEditVehicleForm(false);
            } 
        }

        private void ContextMenuOption2_Click(object sender, EventArgs e)
        {
            if (currentViewId == 0)
            {
                OpenEditStudentForm(true);
            }
            else if (currentViewId == 1)
            {
                OpenInstructorForm(true);
            }
            else if (currentViewId == 2)
            {
                OpenEditVehicleForm(true);
            }
        }

        private void ContextMenuOption3_Click(object sender, EventArgs e)
        {
            if (currentViewId == 0)
            {
                RemoveSelectedStudent();
            }
            else if (currentViewId == 1)
            {
                RemoveSelectedInstructor();
            }
            else if (currentViewId == 2)
            {
                RemoveSelectedVehicle();
            }
        }
        private void ContextMenuOption4_Click(object sender, EventArgs e)
        {
            OpenScheduleForm();
        }

        private void OpenScheduleForm()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView.SelectedRows[0].Index;                        
                if (currentViewId == 0)
                {
                    if (selectedRowIndex >= appManager.Students.Count)
                    {
                        return;
                    }
                    List<StudentActivity> studentActivities;
                    studentActivities = appManager.Schedule.DisplayScheduleForStudent(appManager.Students[selectedRowIndex]);
                    ScheduleForm scheduleForm = new ScheduleForm(studentActivities);
                    scheduleForm.StartPosition = FormStartPosition.CenterParent;
                    scheduleForm.FormClosedEvent += ScheduleForm_FormClosedEvent;
                    scheduleForm.ShowDialog(this);
                }
                else if(currentViewId == 1)
                {
                    if (selectedRowIndex >= appManager.Instructors.Count)
                    {
                        return;
                    }
                    List<PracticalDriving> instructorDrivings;               
                    instructorDrivings = appManager.Schedule.GetInstructorDrivings(appManager.Instructors[selectedRowIndex]);
                    ScheduleForm scheduleForm = new ScheduleForm(instructorDrivings);
                    scheduleForm.StartPosition = FormStartPosition.CenterParent;                  
                    scheduleForm.FormClosedEvent += ScheduleForm_FormClosedEvent;
                    scheduleForm.ShowDialog(this);
                }else if(currentViewId == 2)
                {
                    if (selectedRowIndex >= appManager.Vehicles.Count)
                    {
                        return;
                    }
                    ScheduleForm scheduleForm = new ScheduleForm(appManager.Vehicles[selectedRowIndex]);
                    scheduleForm.StartPosition = FormStartPosition.CenterParent;
                    scheduleForm.FormClosedEvent += ScheduleForm_FormClosedEvent;
                    scheduleForm.ShowDialog(this);
                }               
            }
        }

        private void OpenEditStudentForm(bool isEditing)
        {
            EditStudentForm editForm;
            if(!isEditing)
            {
                editForm = new EditStudentForm(new Student(0,"","","","","B"), isEditing);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.ShowDialog(this);
                return;
            }
            selectedRowIndex = dataGridView.SelectedRows[0].Index;
            if(selectedRowIndex >= appManager.Students.Count) {
                return;
            }
            else
            {
                editForm = new EditStudentForm(appManager.Students[selectedRowIndex], isEditing);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.ShowDialog(this);
            }
        }
        private void OpenInstructorForm(bool isEditing)
        {
            EditInstructorForm editForm;
            if (!isEditing)
            {
                editForm = new EditInstructorForm(new Instructor(0,"","",DateTime.Now,"","",0), isEditing);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.ShowDialog(this);
                return;
            }
            selectedRowIndex = dataGridView.SelectedRows[0].Index;
            if (selectedRowIndex >= appManager.Instructors.Count)
            {
                return;
            }
            else
            {
                editForm = new EditInstructorForm(appManager.Instructors[selectedRowIndex], isEditing);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.ShowDialog(this);
            }           
         
        }
        private void OpenEditVehicleForm(bool isEditing)
        {
            EditVehicleForm editForm;
            if (!isEditing)
            {
                editForm = new EditVehicleForm(new Vehicle(0,"","",0,0,TechnicalCondition.Bad,"B"), isEditing);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.ShowDialog(this);
                return;
            }
            selectedRowIndex = dataGridView.SelectedRows[0].Index;
            if (selectedRowIndex >= appManager.Students.Count)
            {
                return;
            }
            else
            {
                editForm = new EditVehicleForm(appManager.Vehicles[selectedRowIndex], isEditing);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.ShowDialog(this);
            }
        }


        private void RemoveSelectedStudent()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView.SelectedRows[0].Index;
                if (selectedRowIndex >= appManager.Students.Count)
                {
                    return;
                }

                DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć tego kursanta?", "Potwierdzenie usunięcia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int studentId = appManager.Students[selectedRowIndex].Id;
                    appManager.RemoveStudent(studentId);
                }
            }
        }

        private void RemoveSelectedInstructor()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView.SelectedRows[0].Index;
                if (selectedRowIndex >= appManager.Instructors.Count)
                {
                    return;
                }

                DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć tego instruktora?", "Potwierdzenie usunięcia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int instructorId = appManager.Instructors[selectedRowIndex].Id;
                    appManager.RemoveInstructor(instructorId);
                }
            }
        }

        private void RemoveSelectedVehicle()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView.SelectedRows[0].Index;
                if (selectedRowIndex >= appManager.Vehicles.Count)
                {
                    return;
                }

                DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten pojazd?", "Potwierdzenie usunięcia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int vehicleId = appManager.Vehicles[selectedRowIndex].Id;
                    appManager.RemoveVehicle(vehicleId);
                }
            }
        }


        private void SaveToCsvButton_Click(object sender, EventArgs e)
        {
            appManager.SaveDataToCsv();
        }


        private void ScheduleForm_FormClosedEvent(object sender, EventArgs e)
        {       
            this.Close();
        }       
    }
}
