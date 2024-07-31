using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LightningPlus.Data
{
    public class LightningEvent
    {
        public PointLatLng LatitudeLongitudePoint { get; set; } = new(1, 1);

        public double Latitude
        {
            get
            {
                return LatitudeLongitudePoint.Lat;
            }
        }

        public double Longitude
        {
            get
            {
                return LatitudeLongitudePoint.Lng;
            }
        }

        public DateTime DateOfEvent { get; set; } = DateTime.Parse("1960-04-20 00:00:00");

        public double PeakSignal { get; set; } = 500;
        public bool CloudLightning { get; set; } = false;
        public bool CloudToGroundLightning { get; set; } = false;
        public string SensorRecordInformation { get; set; } = "xxx";

        public double ErrorEllipseAngle { get; set; } = 0.0;
        public double SemiMajorAxisLength { get; set; } = 0;
        public double SemiMinorAxisLength { get; set; } = 0;
        public int NumberOfSensorsParticipating { get; set; } = 0;

        public LightningEvent()
        {

        }

        public LightningEvent(double longitude, double latitude, DateTime dateOfEvent, double peakSignal, 
            bool cloudLightning, bool cloudToGroundLightning, string sensorRecordInformation, double errorEllipseAngle, 
            double semiMajorAxisLength, double semiMinorAxisLength, int numberOfSensorsParticipating)
        {
            LatitudeLongitudePoint = new(latitude, longitude);

            DateOfEvent = dateOfEvent;
            PeakSignal = peakSignal;
            CloudLightning = cloudLightning;
            CloudToGroundLightning = cloudToGroundLightning;
            SensorRecordInformation = sensorRecordInformation;
            ErrorEllipseAngle = errorEllipseAngle;
            SemiMajorAxisLength = semiMajorAxisLength;
            SemiMinorAxisLength = semiMinorAxisLength;
            NumberOfSensorsParticipating = numberOfSensorsParticipating;
        }

    }
}
