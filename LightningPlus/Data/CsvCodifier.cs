using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightningPlus.Data
{
    public static class CsvCodifier
    {
        /// <summary>
        /// STROKE ONLY
        /// Expected format is: date, time (nanosecond), latitude, longitude, kiloamps, classification, sensor information, error ellipse angle
        /// semi-major axis length in km, semi-minor axis length in km, number of sensors participating
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static List<string> LoadCsvDocumentForLightningStroke(string filename)
        {
            List<string> csvContents = [];

            if (!File.Exists(filename))
                throw new FileNotFoundException($"Couldn't find {filename}");

            StreamReader _reader = new(filename);

            string? line = string.Empty;

            while((line = _reader.ReadLine()) != null)
            {
                csvContents.Add(line);
            }

            if(csvContents.Count == 0)
            {
                throw new Exception("CsvContents was 0.");
            }

            return csvContents;
        }
    }
}
