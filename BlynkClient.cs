
namespace RaspiBlynkTester.Blynk
{
    public class BlynkClient
    {

        #region members
        private readonly string BlynkAuthToken;
        private readonly HttpClient httpClient;


        private readonly string authToken = "4apTwGJgkaFi6JCO8khZkq_JSBJILiCr";
        private readonly string Base_URL = "https://fra1.blynk.cloud";

        #endregion


        #region constructors
        public BlynkClient()
        {

            BlynkAuthToken = authToken;
            httpClient = new HttpClient();

        }
        #endregion


        #region methods


        /// <summary>
        /// Checks whether the device is connected to the Blynk Server via http
        /// </summary>
        /// <returns></returns>

        public async Task<bool> IsDeviceConnected()
        {
            string DeviceConnctedURL = $"/external/api/isHardwareConnected?token={BlynkAuthToken}";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(Base_URL + DeviceConnctedURL);
                
                if (response.IsSuccessStatusCode)
                {

                    Console.WriteLine($"Erfolgreich verbunden");
                    Log.Warn("Hoi");

                    return true; // Gerät ist verbunden
                }
                else
                {
                    Console.WriteLine($"Nicht verbunden. Status code: {response.StatusCode}");
                    return false; // Gerät ist nicht verbunden

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false; // Fehler trat auf, Gerät ist nicht verbunden
            }
        }


        /// <summary>
        /// Gets Data from the desired Pin via Http
        /// </summary>
        /// <param name="pin">Pins are configured in BlynkPins.cs Pins must be in string Format</param>
        /// <returns></returns>
        public async Task<string> GetData(int pin)
        {
            string GetDataURL = $"/external/api/get?token={BlynkAuthToken}&{"v"+pin.ToString()}";
            try
            {

                HttpResponseMessage response = await httpClient.GetAsync(Base_URL + GetDataURL);

                if (response.IsSuccessStatusCode)
                {

                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Data erhalten auf dem Pin {"v" + pin} ist der Wert ");
                    Console.WriteLine(content);
                    return content;
                }
                else
                {
                    Console.WriteLine($"Get Data: Nicht verbunden. Status code: {response.StatusCode}");
                    return response.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Sends Data to the desired Pin via http
        /// </summary>
        /// <param name="pin">>Pins are configured in BlynkPins.cs</param>
        /// <param name="value">Value must be in string format</param>
        /// <returns></returns>
        public async Task DataToPin(int pin, string value)
        {
            string SendValueToPinURL = $"/external/api/update?token={BlynkAuthToken}&{"v"+pin.ToString()}={value}";


            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(Base_URL + SendValueToPinURL);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Value {value} sent to pin {pin} successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to send value to pin {pin}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


       

        #endregion



    }

}
