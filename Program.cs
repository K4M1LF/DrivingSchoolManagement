using DrivingSchoolManagement;

AppManager.GetInstance().databaseManager.ConnectAndDoSomething();


Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new Form1());