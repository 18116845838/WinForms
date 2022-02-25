using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliect
{
	public partial class Form1 : Form
	{
		Socket socket;
		public Form1()
		{
			InitializeComponent();

		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				socket.Close();
				textBox3.AppendText("远程主机关闭成功");
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				IPAddress ip = IPAddress.Parse(textBox1.Text);
				IPEndPoint ips = new IPEndPoint(ip, int.Parse(textBox2.Text));
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Connect(ips);
				textBox3.AppendText("\n远程主机连接成功");
				Thread thread = new Thread(GetMessage);
				;
				thread.IsBackground = true;
				thread.Start();
			}
			catch (Exception)
			{

				throw;
			}
			

		}
		void GetMessage()
		{
			try
			{
				while (true)
				{
					byte[] buffer = new byte[1024 * 1024 * 2];
					int r = socket.Receive(buffer);
					if (r == 0)
					{
						break;
					}
					string str = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
					textBox3.AppendText("\n" + str);
				}
			}
			catch (Exception)
			{

			}
			

		}
		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox4_TextChanged(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				string msg = textBox4.Text;
				byte[] vs = Encoding.UTF8.GetBytes(msg);
				socket.Send(vs, vs.Length, 0);
			}
			catch (Exception)
			{
//''HB
			}
			
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Control.CheckForIllegalCrossThreadCalls = false;

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			socket.Close();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			socket.Close();
		}
	}
}
