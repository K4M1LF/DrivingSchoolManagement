using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DrivingSchoolManagement
{
    public partial class ScheduleForm : Form
    {
        public event EventHandler FormClosedEvent;

        private DataGridView dataGridView;
        private Button planDrivingButton;
        private Button addIncidentsButton;
        Vehicle Vehicle;
        private AppManager appManager;
        public ScheduleForm(List<StudentActivity> studentActivities)
        {
            appManager = AppManager.GetInstance();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeStudentComponents();
            dataGridView.Rows.Clear();
            DisplayStudentActivities(studentActivities);
        }

        public ScheduleForm(List<PracticalDriving> instructorDrivings)
        {
            appManager = AppManager.GetInstance();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeInstructorComponents();
            dataGridView.Rows.Clear();
            DisplayInstructorActivities(instructorDrivings);

        }

        public ScheduleForm(Vehicle vehicle)
        {
            appManager = AppManager.GetInstance();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeIncidentComponents();
            dataGridView.Rows.Clear();
            DisplayIncidents(vehicle);
            Vehicle = vehicle;

        }

        private void InitializeStudentComponents()
        {
            this.Size = new System.Drawing.Size(600, 500);
            this.Text = "Harmonogram Kursanta";

            dataGridView = new DataGridView();
            dataGridView.Size = new System.Drawing.Size(550, 300);
            dataGridView.Location = new System.Drawing.Point(20, 30);
            this.Controls.Add(dataGridView);

            dataGridView.Columns.Add("ActivityID", "ID");
            dataGridView.Columns.Add("InstructorFirstName", "Imię Instruktora");
            dataGridView.Columns.Add("InstructorLastName", "Nazwisko Instruktora");
            dataGridView.Columns.Add("StartDate", "Data Rozpoczęcia");
            dataGridView.Columns.Add("ActivityType", "Typ Aktywności");
            dataGridView.Rows.Clear();

           
            planDrivingButton = new Button();
            planDrivingButton.Location = new System.Drawing.Point(20, 350);
            planDrivingButton.Size = new System.Drawing.Size(150, 30);
            planDrivingButton.Text = "Zaplanuj Jazdę";
            planDrivingButton.Click += PlanDrivingButton_Click;
            this.Controls.Add(planDrivingButton);
        }

        private void InitializeInstructorComponents()
        {
            this.Size = new System.Drawing.Size(600, 400);
            this.Text = "Harmonogram Instruktora";

            dataGridView = new DataGridView();
            dataGridView.Size = new System.Drawing.Size(550, 300);
            dataGridView.Location = new System.Drawing.Point(20, 30);
            this.Controls.Add(dataGridView);

            dataGridView.Columns.Add("ActivityID", "ID");
            dataGridView.Columns.Add("StudentFirstName", "Imię Kursanta");
            dataGridView.Columns.Add("StudentLastName", "Nazwisko Kursanta");
            dataGridView.Columns.Add("StartDate", "Data Rozpoczęcia");
            dataGridView.Columns.Add("ActivityType", "Typ Aktywności");
            dataGridView.Rows.Clear();
        }

        private void InitializeIncidentComponents()
        {
            this.Size = new System.Drawing.Size(600, 500);
            this.Text = "Nowy Incydent";

            dataGridView = new DataGridView();
            dataGridView.Size = new System.Drawing.Size(550, 300);
            dataGridView.Location = new System.Drawing.Point(20, 30);
            this.Controls.Add(dataGridView);

            dataGridView.Columns.Add("IncidentID", "ID");
            dataGridView.Columns.Add("IncidentDate", "Data Wypadku");
            dataGridView.Columns.Add("IncidentDescription", "Opis Wypadku");
            dataGridView.Columns.Add("RepairCost", "Koszt Naprawy");
            dataGridView.Columns.Add("IsRepaired", "Stan");
            dataGridView.Rows.Clear();


            addIncidentsButton = new Button();
            addIncidentsButton.Location = new System.Drawing.Point(200, 350);
            addIncidentsButton.Size = new System.Drawing.Size(150, 30);
            addIncidentsButton.Text = "Nowy Indcydent";
            addIncidentsButton.Click += ShowIncidentsButton_Click;
            this.Controls.Add(addIncidentsButton);
        }

        private void DisplayStudentActivities(List<StudentActivity> studentActivities)
        {
            dataGridView.Rows.Clear();
            foreach (var activity in studentActivities)
            {
                dataGridView.Rows.Add(activity.ActivityID, activity.InstructorFirstName, activity.InstructorLastName, activity.StartDate, activity.ActivityType);
            }
        }

        private void DisplayInstructorActivities(List<PracticalDriving> instructorDrivings)
        {
            dataGridView.Rows.Clear();
            foreach (var activity in instructorDrivings)
            {
                dataGridView.Rows.Add(activity.Id, activity.Student.FirstName, activity.Student.LastName, activity.StartTime, "Jazda Praktyczna");
            }
        }

        private void DisplayIncidents(Vehicle vehicle)
        {
            dataGridView.Rows.Clear();
            vehicle.Incidents = appManager.databaseManager.GetIncidentsForVehicle(vehicle.Id);
            foreach (Incident incident in vehicle.Incidents)
            {
                string state = incident.IsRepaired ? "Naprawiony" : "Oczekujący";
                dataGridView.Rows.Add(incident.Id, incident.Date, incident.Description, incident.RepairCost, state);
            }
        }

        private void PlanDrivingButton_Click(object sender, EventArgs e)
        {
            var scheduleDrivingForm = new ScheduleDrivingForm();
            scheduleDrivingForm.Show();
        }


        private void ShowIncidentsButton_Click(object sender, EventArgs e)
        {
            var addIncidentsForm = new AddIncidentForm(Vehicle);
            addIncidentsForm.Show();
        }

        private void ScheduleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
