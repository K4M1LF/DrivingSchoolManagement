using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DrivingSchoolManagement
{

    public class CsvManager
    {
        public static void SaveToCsv<T>(List<T> data, string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    var properties = typeof(T).GetProperties();
                    sw.WriteLine(string.Join(',', properties.Select(p => p.Name)));

                    foreach (var item in data)
                    {
                        var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "");
                        sw.WriteLine(string.Join(',', values));
                    }
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania danych do pliku CSV: {ex.Message}");
            }
        }
    }

}
