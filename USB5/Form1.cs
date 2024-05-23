using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbLibrary;


namespace USB5
{
    public partial class Form1 : Form
    {
        byte[] hello = new byte[2];
        byte[] goodbye = new byte[2];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 3; i <= 20; i++)
            {
                comboBox1.Items.Add(i);
                comboBox2.Items.Add(i);
            }



            comboBox1.SelectedIndex = 7; 
            comboBox2.SelectedIndex = 7; 

            usbHidPort.VendorId = 0x04D8;
            usbHidPort.ProductId = 0x0001;

            usbHidPort.CheckDevicePresent(); 


        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            usbHidPort.RegisterHandle(Handle);
        }

        protected override void WndProc(ref Message m)
        {
            usbHidPort.ParseMessages(ref m);
            base.WndProc(ref m);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you want exits? ", "Quesion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void usbHidPort_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new DataRecievedEventHandler(usbHidPort_OnDataRecieved), new object[] { sender, args });
                }
                catch
                { }
            }
            else
            {
                if (usbHidPort.SpecifiedDevice != null)
                {

                    hello = args.data;
                    if (hello[1] == 't')
                    {
 
                        pictureBox1.Image = USB5.Properties.Resources.on;
                    }
                    else if (hello[1] == 'w')
                    {
                        pictureBox2.Image = USB5.Properties.Resources.on;
                    }
                    else if (hello[1] == 'r')
                    {
                        pictureBox3.Image = USB5.Properties.Resources.on;
                        bntoff.Enabled = true;
                        bntoff1.Enabled = true;
                        bntoff2.Enabled = true;
                        bntoff3.Enabled = true;
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;



                    }
                    else if (hello[1] == '2')
                    {
                        pictureBox1.Image = USB5.Properties.Resources.ledoff;
                    }
                    else if (hello[1] == '4')
                    {
                        pictureBox2.Image = USB5.Properties.Resources.ledoff;
                    }
                    else if (hello[1] == '6')
                    {
                        pictureBox3.Image = USB5.Properties.Resources.ledoff;
                       

                    }
                    else if (hello[1] == 's')
                    {
                        
                        pictureBox1.Image = USB5.Properties.Resources.ledoff;
                        pictureBox2.Image = USB5.Properties.Resources.ledoff;
                        pictureBox3.Image = USB5.Properties.Resources.ledoff;
                    }
                }
            }
        }


        private void usbHidPort_OnDataSend(object sender, EventArgs e)
        {

        }

        private void usbHidPort_OnDeviceArrived(object sender, EventArgs e)
        {

        }

        private void usbHidPort_OnDeviceRemoved(object sender, EventArgs e)
        {

        }

        private void usbHidPort_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {

            
            textBox1.Text = " Connected";
            textBox1.BackColor = Color.Lime;

            pictureBox1.Image = USB5.Properties.Resources.ledoff;
            pictureBox2.Image = USB5.Properties.Resources.ledoff;
            pictureBox3.Image = USB5.Properties.Resources.ledoff;

        }

        private void usbHidPort_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(usbHidPort_OnSpecifiedDeviceRemoved), new object[] { sender, e });
            }
            else
            {
                textBox1.Text = " Disconnected !";
                textBox1.BackColor = Color.Red;

                pictureBox1.Image = USB5.Properties.Resources.ledoff;
                pictureBox2.Image = USB5.Properties.Resources.ledoff;
                pictureBox3.Image = USB5.Properties.Resources.ledoff;
            }
        }
        //START
        private void bnton_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            bntoff.Enabled = false;
            bntoff1.Enabled = false;
            bntoff2.Enabled = false;
            bntoff3.Enabled = false;

            if (usbHidPort.SpecifiedDevice != null)

            {

                goodbye[1] = (byte)'y';
                usbHidPort.SpecifiedDevice.SendData(goodbye);
                timer1.Interval = int.Parse(comboBox1.SelectedItem.ToString()) * 1000;
                timer1.Enabled = true;
                timer2.Interval = int.Parse(comboBox2.SelectedItem.ToString()) * 1000;
                
            }

            else
            {
                MessageBox.Show("Decive not found. Please reconnect USB device to use.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void bntoff_Click(object sender, EventArgs e)
        {
            bntoff.Enabled = false;
            bntoff1.Enabled = false;
            bntoff2.Enabled = false;
            bntoff3.Enabled = false;


            if (usbHidPort.SpecifiedDevice != null)
            {
                goodbye[1] = (byte)'a';
                usbHidPort.SpecifiedDevice.SendData(goodbye);

            }
            else
            {
                MessageBox.Show("Decive not found. Please reconnect USB device to use.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bntoff1_Click(object sender, EventArgs e)
        {
            if (usbHidPort.SpecifiedDevice != null)
            {
                
                
               goodbye [1] = (byte)'1';
                usbHidPort.SpecifiedDevice.SendData(goodbye);

            }
            else
            {
                MessageBox.Show("Decive not found. Please reconnect USB device to use.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bntoff2_Click(object sender, EventArgs e)
        {
            if (usbHidPort.SpecifiedDevice != null)
            {
             
                goodbye[1] = (byte)'3';
                usbHidPort.SpecifiedDevice.SendData(goodbye);

            }
            else
            {
                MessageBox.Show("Decive not found. Please reconnect USB device to use.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bntoff3_Click(object sender, EventArgs e)
        {
            


            if (usbHidPort.SpecifiedDevice != null)
            {
                goodbye[1] = (byte)'5';
                usbHidPort.SpecifiedDevice.SendData(goodbye);

            }
            else
            {
                MessageBox.Show("Decive not found. Please reconnect USB device to use.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      

        private void buttoncan1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 7;
        }

        private void buttoncan2_Click(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            comboBox2.SelectedIndex = 7;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            goodbye[1] = (byte)'q';
            usbHidPort.SpecifiedDevice.SendData(goodbye);
            timer1.Enabled = false;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            goodbye[1] = (byte)'e';
            usbHidPort.SpecifiedDevice.SendData(goodbye);
            timer2.Enabled = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
