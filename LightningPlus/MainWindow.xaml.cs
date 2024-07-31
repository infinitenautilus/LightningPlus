using System.Text;
using System.Windows;
using GMap.NET.Projections;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using GMap.NET.MapProviders;
using LightningPlus.Data;
using System.Windows.Input;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using Microsoft.Win32;
using System.Reflection.PortableExecutable;

namespace LightningPlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupMapControl();
        }

        private void SetupMapControl()
        {
            _MapControl.MapProvider = GMapProviders.OpenStreetMap;
            _MapControl.Position = new PointLatLng(40, -102);
            _MapControl.MinZoom = 3;
            _MapControl.MaxZoom = 18;
            _MapControl.Zoom = 18;
            _MapControl.ShowCenter = false;
            _MapControl.MouseMove += GMapMouseMoveEvent;
            _MapControl.DragButton = MouseButton.Left;

            GMaps.Instance.Mode = AccessMode.CacheOnly;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GMapMouseMoveEvent(object sender, MouseEventArgs e)
        {
            
            var mousePosition = e.GetPosition(_MapControl);
            
            PointLatLng position = _MapControl.FromLocalToLatLng((int)mousePosition.X, (int)mousePosition.Y);

            StatusBarTextBlockLatitudeLongitude.Text = $"Latitude: {Math.Round(position.Lat, 4)}, Longitude: {Math.Round(position.Lng, 4)}";
        }

        private void Edit_InsertStrokeClick(object sender, RoutedEventArgs e) 
        { 
            LightningEvent levent = new(-98.8093, 29.7506, DateTime.Parse("2023-02-08 11:23:06.371"), 4.5, 
                true, false, "AxT", 149, 0.2, 0.2, 3);
        }

        private void LoadCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files(*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if(result == true)
            {
                string filename = openFileDialog.FileName;
                StatusBarOutput.Content = filename;

                try
                {
                    //List<string> csvData = ParseCsv(filename);

                    List<string> csvFileData = CsvCodifier.LoadCsvDocumentForLightningStroke(filename);
                    StatusBarOutput.Content = csvFileData.Count;

                    while(csvFileData.Count > 0)
                    {
                        string line = csvFileData.First();
                        
                        string[] words = line.Split(',');

                        LightningEvent le = new();
                        try
                        {
                            le.DateOfEvent = DateTime.Parse(words[0]);

                            le.LatitudeLongitudePoint = new PointLatLng(double.Parse(words[2]), double.Parse(words[3]));
                            le.PeakSignal = double.Parse(words[4]);
                            
                            switch(words[5])
                            {
                                case "C":
                                    le.CloudLightning = true;
                                    le.CloudToGroundLightning = false;
                                    break;
                                case "G":
                                    le.CloudLightning = false;
                                    le.CloudToGroundLightning = true;
                                    break;
                                default:
                                    le.CloudLightning = false;
                                    le.CloudToGroundLightning = false;
                                    break;
                            }

                            le.SensorRecordInformation = words[6];
                            le.ErrorEllipseAngle = double.Parse(words[7]);
                            le.SemiMajorAxisLength = double.Parse(words[8]);
                            le.SemiMinorAxisLength = double.Parse(words[9]);

                            le.NumberOfSensorsParticipating = int.Parse(words[10]);


                            //MessageBox.Show($"{le.Latitude}");

                            GMapMarker marker = new(le.LatitudeLongitudePoint)
                            {
                                Shape = new System.Windows.Shapes.Ellipse
                                {
                                    Width = 0.5,
                                    Height = 0.5,
                                    Stroke = System.Windows.Media.Brushes.Red,
                                    StrokeThickness = 1,
                                    
                                }
                            };
                            
                            
                            _MapControl.Markers.Add(marker);

                            csvFileData.Remove(csvFileData.First());
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show($"Exception: {ex.Message}");
                        }
                    }
                    

                    //MessageBox.Show($"{csvData[0]}");
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"EXCEPTION: {ex.Message}");
                }
            }
            else
            {
                StatusBarOutput.Content = "Unable to find file.";
            }
        }


        
    }
}