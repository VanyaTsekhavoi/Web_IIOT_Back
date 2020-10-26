using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using Modbus.Device;
using Modbus.Utility;
using System.Threading;

namespace Controllers_Layer
{
    public class Logic
    {
        SerialPort port;
        IModbusSerialMaster master;

        // Slaves
        modbusDevice PLK;
        modbusDevice arduino;
        modbusDevice arduinoVolt;

        bool volt;

        bool firstRelayState;
        bool secondRelayState;

        bool firstSignalingState;
        bool secondSignalingState;

        //States
        bool monitorButton = true,
            setFirstTButton = true,
            setSecondTButton = true,
            firstRelayButton = true,
            secondRelayButton = true,
            voltButton = true;

        public string arduinoTemperature, arduinoHumidity;
        public string firstBoxTemperature, firstBoxPressure, firstBoxHumidity;
        public string secondBoxTemperature, secondBoxPressure, secondBoxHumidity;

        private static System.Timers.Timer Timer { get; set; }
        private List<string> portsComboBox = new List<string>(0);

        public static string status = null;
        public int parent;

        public Logic()
        {
            StartProgect();
            parent = 0;
        }

        public string StartProgect()
        {
            //Get Ports
            string[] ports = SerialPort.GetPortNames();
            foreach (string p in ports)
                portsComboBox.Add(p);



            // Slaves
            PLK = new modbusDevice(1, false);
            arduino = new modbusDevice(3, false);
            arduinoVolt = new modbusDevice(4, false);

            volt = true;
            //voltButton.Text = "Switch(OFF)";

            firstRelayState = false;
            secondRelayState = false;

            firstSignalingState = false;
            secondSignalingState = false;

            try
            {
                Connect();

                SetTimer();

                //Timer.Stop();
                //Timer.Dispose();
            }
            catch(Exception ex)
            {
                status = ex.ToString();
                return status;
            }

            Console.WriteLine("-----Successful  connection-----");

            status = "ok";
            return status;
        }

        private void Connect()
        {
            if (PLK.isConnected)
            {
                Console.WriteLine("-----You've already connected-----");
                return;
            }
            try
            {
                // Port
                port = new SerialPort(portsComboBox[0].ToString());
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.ReadTimeout = 10000;
                port.WriteTimeout = 10000;

                port.Open();
            }
            catch(UnauthorizedAccessException)
            {
                Console.WriteLine("-----No Cop-port access-----");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("-----Computer is not connected to any of Com's.-----");
            }
            if (port.IsOpen)
            {
                PLK.isConnected = true;
                arduino.isConnected = true;
                master = ModbusSerialMaster.CreateRtu(port);
                monitorButton = true;
                setFirstTButton = true;
                setSecondTButton = true;
                firstRelayButton = true;
                secondRelayButton = true;
                voltButton = true;
            }
            else
            {
                Console.WriteLine("-----Impossible to connect to Com-----");
            }
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            Timer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            Timer.Enabled = true;
            Timer.Elapsed += timer1_Tick;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            parent++;
            Console.WriteLine("---Get Data Tick---");
            // Arduino
            arduinoTemperature = getArduinoTemperature();
            arduinoHumidity = getArduinoHumidity();


            // PLK
            firstBoxTemperature = getFirstTemperature();
            firstBoxPressure = getFirstPressure();
            firstBoxHumidity = getFirstHumidity();
            secondBoxTemperature = getSecondTemperature();
            secondBoxPressure = getSecondPressure();
            secondBoxHumidity = getSecondHumidity();

            {
                //firstSignalingState = checkFirstAccess();
                //secondSignalingState = checkSecondAccess();
                //if (firstSignalingState)
                //{
                //    firstSignaliningStateIndicatorComponent.StateIndex = 1;
                //}
                //else
                //{
                //    firstSignaliningStateIndicatorComponent.StateIndex = 3;
                //}

                //if (secondSignalingState)
                //{
                //    secondSignalingStateIndicatorComponent.StateIndex = 1;
                //}
                //else
                //{
                //    secondSignalingStateIndicatorComponent.StateIndex = 3;
                //}
            }

        }

        public string getFirstTemperature()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(PLK.slaveAddress, 0, 2);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----System doesn't connect or there are some channel issues.-----");
            }
            ushort[] reg = new ushort[2];
            reg[0] = registers[1];
            reg[1] = registers[0];
            return convertToDouble(reg, 0).ToString();
        }

        public string getFirstPressure()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(PLK.slaveAddress, 7, 2);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----System doesn't connect or there are some channel issues.-----");
            }
            ushort[] reg = new ushort[2];
            reg[0] = registers[1];
            reg[1] = registers[0];
            return convertToDouble(reg, 0).ToString();
        }

        public string getFirstHumidity()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(PLK.slaveAddress, 11, 2);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----System doesn't connect or there are some channel issues.-----");
            }
            ushort[] reg = new ushort[2];
            reg[0] = registers[1];
            reg[1] = registers[0];
            return convertToDouble(reg, 0).ToString();
        }

        public string getSecondTemperature()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(PLK.slaveAddress, 2, 2);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----System doesn't connect or there are some channel issues.-----");
            }
            ushort[] reg = new ushort[2];
            reg[0] = registers[1];
            reg[1] = registers[0];
            return convertToDouble(reg, 0).ToString();
        }

        public string getSecondPressure()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(1, 9, 2);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----System doesn't connect or there are some channel issues.-----");
            }
            ushort[] reg = new ushort[2];
            reg[0] = registers[1];
            reg[1] = registers[0];
            return convertToDouble(reg, 0).ToString();
        }

        public string getSecondHumidity()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(1, 13, 2);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----System doesn't connect or there are some channel issues.-----");
            }
            ushort[] reg = new ushort[2];
            reg[0] = registers[1];
            reg[1] = registers[0];
            return convertToDouble(reg, 0).ToString();
        }

        public string getArduinoTemperature()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(arduino.slaveAddress, 0, 1);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----Arduino doesn't connect or there are some channel issues.-----");
            }
            float temp = registers[0];
            return temp.ToString();
        }

        public string getArduinoHumidity()
        {
            ushort[] registers = { 0, };
            try
            {
                registers = master.ReadInputRegisters(arduino.slaveAddress, 1, 1);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("-----Arduino doesn't connect or there are some channel issues.-----");
            }
            float hum = registers[0];
            return hum.ToString();
        }
        public static float convertToDouble(ushort[] registers, int startReg)
        {
            int intValue = (int)registers[startReg];
            intValue <<= 16;
            intValue += (int)registers[startReg + 1];
            return (float)Math.Round(BitConverter.ToSingle(BitConverter.GetBytes(intValue), 0), 2);
        }

        public ushort[] convertToUShortArray(float number)
        {
            int int32 = BitConverter.ToInt32(BitConverter.GetBytes((float)Math.Round((double)number, 3)), 0);
            int num = int32 >> 16;
            return new ushort[2]
            {
                (ushort) num,
                (ushort) (int32 - (num << 16))
            };
        }

        //public void setTemperatureForFirstStorage(float number)
        //{
        //    master.WriteMultipleRegisters(PLK.slaveAddress, (ushort)0, convertToUShortArray(number));
        //}

        //public void setTemperatureForSecondStorage(float number)
        //{
        //    master.WriteMultipleRegisters(PLK.slaveAddress, (ushort)2, this.convertToUShortArray(number));
        //}

        public void openFirstRelay()
        {
            byte[] crc = getCRC(new byte[6]
            {
                (byte) 2,
                (byte) 6,
                (byte) 0,
                (byte) 1,
                (byte) 1,
                (byte) 0
            });
            port.Write(crc, 0, crc.Length);
        }

        public void closeFirstRelay()
        {
            byte[] crc = this.getCRC(new byte[6]
            {
                (byte) 2,
                (byte) 6,
                (byte) 0,
                (byte) 1,
                (byte) 2,
                (byte) 0
            });
            port.Write(crc, 0, crc.Length);
        }

        public void openSecondRelay()
        {
            byte[] crc = this.getCRC(new byte[6]
            {
                (byte) 2,
                (byte) 6,
                (byte) 0,
                (byte) 2,
                (byte) 1,
                (byte) 0
             });
            port.Write(crc, 0, crc.Length);
        }

        public void closeSecondRelay()
        {
            byte[] crc = this.getCRC(new byte[6]
            {
                (byte) 2,
                (byte) 6,
                (byte) 0,
                (byte) 2,
                (byte) 2,
                (byte) 0
            });
            port.Write(crc, 0, crc.Length);
        }

        public byte[] getCRC(byte[] arr)
        {
            byte[] crc = ModbusUtility.CalculateCrc(arr);
            byte[] numArray = new byte[8];
            for (int index = 0; index < 8; ++index)
                numArray[index] = index >= 6 ? crc[index - 6] : arr[index];
            return numArray;
        }


        //public bool checkFirstAccess()
        //{
        //    return master.ReadCoils((byte)1, (ushort)0, (ushort)1)[0];
        //}

        //public bool checkSecondAccess()
        //{
        //    return master.ReadCoils((byte)1, (ushort)1, (ushort)1)[0];
        //}


        //private void setFirstTButton_Click(object sender, EventArgs e)
        //{
        //    setTemperatureForFirstStorage(Convert.ToSingle(firstTTextBox.Text));
        //}

        //private void setSecondTButton_Click(object sender, EventArgs e)
        //{
        //    setTemperatureForSecondStorage(Convert.ToSingle(secondTTextBox.Text));
        //}

        public void logicFirstrelay()
        {
            firstRelay();
        }

        private void firstRelayButton_Click(object sender, EventArgs e)
        {
            firstRelay();
        }

        private void firstRelay()
        {
            try
            {
                if (firstRelayState == false)
                {
                    firstRelayState = true;
                    openFirstRelay();
                }
                else
                {
                    firstRelayState = false;
                    closeFirstRelay();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("-----Rele Timeout.-----");
            }
        }

        public void logicSecondRelay()
        {
            secondRelay();
        }

        private void secondRelayButton_Click(object sender, EventArgs e)
        {
            secondRelay();
        }

        private void secondRelay()
        {
            try
            {
                if (secondRelayState == false)
                {
                    secondRelayState = true;
                    openSecondRelay();
                }
                else
                {
                    secondRelayState = false;
                    closeSecondRelay();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("-----Rele Timeout.-----");
            }
        }

        private void voltButton_Click(object sender, EventArgs e)
        {
            giveArduinoVolt();
        }

        public void logicArduinoVolt()
        {
            giveArduinoVolt();
        }

        private void giveArduinoVolt()
        {
            if (volt)
            {
                Command(1);
                volt = false;
                //voltButton.Text = "Switch(ON)";
            }
            else
            {
                Command(0);
                volt = true;
                //voltButton.Text = "Switch(OFF)";
            }
        }

        public void Command(byte voltage)
        {
            byte[] crc = getCRC(new byte[6]
            {
                4,
                6,
                0,
                0,
                0,
                voltage
            });
            try
            {
                port.Write(crc, 0, crc.Length);
            }
            catch (Exception)
            {
                Console.WriteLine("-----Arduino Semistor Timeout.-----");
            }
        }
    }

    public class modbusDevice
    {
        public byte slaveAddress;
        public bool isConnected = false;

        public modbusDevice(byte slaveAddress, bool isConnected)
        {
            this.slaveAddress = slaveAddress;
            this.isConnected = isConnected;
        }

    }
}
