using System;
using System.Windows.Forms;

namespace DrivingSchoolManagement
{
    public partial class AddIncidentForm : Form
    {
        private DateTimePicker dateDateTimePicker;
        private TextBox descriptionTextBox;
        private TextBox repairCostTextBox;
        private CheckBox repairedCheckBox;
        private Button saveButton;

        private Vehicle vehicle;

        public AddIncidentForm(Vehicle vehicle)
        {
            InitializeComponent();
            this.vehicle = vehicle;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Size = new System.Drawing.Size(400, 250);
            this.Text = "Dodaj Incydent";

            dateDateTimePicker = new DateTimePicker();
            dateDateTimePicker.Location = new System.Drawing.Point(150, 20);
            this.Controls.Add(dateDateTimePicker);

            descriptionTextBox = new TextBox();
            descriptionTextBox.Location = new System.Drawing.Point(150, 60);
            descriptionTextBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(descriptionTextBox);

            repairCostTextBox = new TextBox();
            repairCostTextBox.Location = new System.Drawing.Point(150, 100);
            repairCostTextBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(repairCostTextBox);

            repairedCheckBox = new CheckBox();
            repairedCheckBox.Text = "Naprawiony";
            repairedCheckBox.Location = new System.Drawing.Point(150, 140);
            this.Controls.Add(repairedCheckBox);

            saveButton = new Button();
            saveButton.Text = "Zapisz";
            saveButton.Location = new System.Drawing.Point(150, 180);
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            AddLabel("Data Incydentu:", 20, 20);
            AddLabel("Opis:", 20, 60);
            AddLabel("Koszt Naprawy:", 20, 100);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new System.Drawing.Point(x, y);
            label.AutoSize = true;
            this.Controls.Add(label);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DateTime date = dateDateTimePicker.Value;
            string description = descriptionTextBox.Text;
            int repairCost;
            bool isRepaired = repairedCheckBox.Checked;

            if (!int.TryParse(repairCostTextBox.Text, out repairCost))
            {
                MessageBox.Show("Podaj poprawny koszt naprawy.");
                return;
            }
            Incident newIncident = new Incident(vehicle.Incidents.Count + 1,vehicle.Id, date, description, repairCost,isRepaired);
            AppManager.GetInstance().AddIncident(vehicle.Id, newIncident);
            MessageBox.Show("Dodano nowy incydent.");

            this.Close();
        }
    }
}
