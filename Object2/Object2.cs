using System.IO.Pipes;

namespace Object2
{
    public partial class Object2 : Form
    {

        private NamedPipeClientStream clientStream;
        public Object2(int n, double minValue, double maxValue)
        {
            InitializeComponent();
            StartPipeClient();

            // Start a separate thread to continuously listen for incoming data
            Thread receiveThread = new Thread(ReceiveData);
            receiveThread.Start();
            //LoadGeneratedList(n, minValue, maxValue);

        }

        private void StartPipeClient()
        {
            // Connect to the named pipe created by App1
            clientStream = new NamedPipeClientStream(".", "Lab6_Pipe", PipeDirection.In);
            clientStream.Connect();
        }



        private void ReceiveData()
        {
            try
            {
                while (true)
                {
                    // Read data from the pipe
                    byte[] buffer = new byte[4096]; // Adjust the buffer size as needed
                    int bytesRead = clientStream.Read(buffer, 0, buffer.Length);

                    // Extract the values from the received byte array
                    int receivedIntValue = BitConverter.ToInt32(buffer, 0);
                    double receivedDoubleValue1 = BitConverter.ToDouble(buffer, sizeof(int));
                    double receivedDoubleValue2 = BitConverter.ToDouble(buffer, sizeof(int) + sizeof(double));



                    // Invoke UI-related operations on the main UI thread
                    Invoke(new Action(() =>
                    {

                        LoadGeneratedList(receivedIntValue, receivedDoubleValue1, receivedDoubleValue2);
                        Thread.Sleep(1000);
                        SignalObject1();
                        // Display or use the received values as needed
                        //MessageBox.Show($"Received data:\nInt: {receivedIntValue}\nDouble 1: {receivedDoubleValue1}\nDouble 2: {receivedDoubleValue2}");
                    }));


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving data: " + ex.Message);
            }
        }

        private void SignalObject1()
        {
            try
            {
                // Assuming "EventObject2To1" is the event name shared between Object2 and Object1
                using (EventWaitHandle eventHandle = EventWaitHandle.OpenExisting("EventObject2To1"))
                {
                    eventHandle.Set();
                }
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                // Handle the case where the event doesn't exist (perhaps Object1 hasn't started yet)
                // You might want to create the event here if needed
                // eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, "EventObject2To1");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error signaling Object1: " + ex.Message);
            }
        }

            private void LoadGeneratedList(int n, double minValue, double maxValue)
        {
            if (n == 0)
            {
                listBox1.Items.Add("n is set to 0");
            }
            else if (n >= 1 && n <= 100)
            {
                double[] vectorOfDoubles = GenerateRandomDoubles(n, minValue, maxValue);

                DisplayDoublesInListBox(vectorOfDoubles);
                CopyDoublesToClipboard(vectorOfDoubles);


            }
            else
            {
                listBox1.Items.Add("Invalid value for n. It should be between 1 and 100.");
            }
        }

        private void DisplayDoublesInListBox(double[] vectorOfDoubles)
        {
            listBox1.Items.Clear();

            foreach (double value in vectorOfDoubles)
            {
                listBox1.Items.Add(value.ToString());
            }
        }

        private double[] GenerateRandomDoubles(int n, double minValue, double maxValue)
        {
            Random random = new Random();
            double[] result = new double[n];

            for (int i = 0; i < n; i++)
            {
                double randomValue = random.NextDouble() * (maxValue - minValue) + minValue;
                result[i] = Math.Round(randomValue, 2);
            }

            return result;
        }


        private void CopyDoublesToClipboard(double[] doubleArray)
        {
            string doubleArrayString = string.Join(';', doubleArray);

            Clipboard.SetText(doubleArrayString);

            if (Clipboard.ContainsText())
            {
                Console.WriteLine("Array of doubles has been copied to the Clipboard.");
            }
            else
            {
                Console.WriteLine("Failed to copy array of doubles to the Clipboard.");
            }
        }
    }

}