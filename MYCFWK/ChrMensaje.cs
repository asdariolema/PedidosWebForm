using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace lahres_AdminHotel
{
	/// <summary>
	/// Summary description for OctoMensaje.
	/// </summary>
	public class ChrMensaje : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.IContainer components;

		private Color wDefaultColor = Color.LightSkyBlue;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label lblMensaje;
		private Color wDefaultColorError = Color.Yellow;


		public ChrMensaje()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.Visible = false;
			this.Height = 44;
		}
	
		public enum TipoError
		{
			Information,
			Warning,
			Error,
			Question,
			None
		}
					  

		private TipoError mensajeTipo;
		public TipoError MensajeTipo
		{
			set 
			{
				mensajeTipo = value;
				setControl();
			}
			
		}

		private string textomMensaje;
		public string TextoMensaje
		{
			set 
			{
				textomMensaje = value;
				setControl();
			}
		}

		private Color colorFondo;
		public Color ColorFondo
		{
			set
			{
				colorFondo = value;
				this.BackColor = colorFondo;
			}
		}

		private Color colorTexto;
		public Color ColorTexto
		{
			set
			{
				colorTexto = value;
				lblMensaje.ForeColor = colorTexto;
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OctoMensaje));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.lblMensaje = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(30, 30);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// picIcon
			// 
			this.picIcon.BackColor = System.Drawing.Color.LightSkyBlue;
			this.picIcon.Location = new System.Drawing.Point(5, 3);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(30, 30);
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// lblMensaje
			// 
			this.lblMensaje.Location = new System.Drawing.Point(35, 0);
			this.lblMensaje.Name = "lblMensaje";
			this.lblMensaje.Size = new System.Drawing.Size(318, 39);
			this.lblMensaje.TabIndex = 1;
			this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// OctoMensaje
			// 
			this.BackColor = System.Drawing.Color.LightSkyBlue;
			this.Controls.Add(this.lblMensaje);
			this.Controls.Add(this.picIcon);
			this.Name = "OctoMensaje";
			this.Size = new System.Drawing.Size(357, 39);
			this.Load += new System.EventHandler(this.OctoMensaje_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void OctoMensaje_Load(object sender, System.EventArgs e)
		{
		}
		
		private void setControl()
		{
//			if(mensajeTipo != TipoError.None)
//				this.Visible = true;
//			else
//				this.Visible = false;

			if(textomMensaje != null && textomMensaje.Length > 0)
			{
				lblMensaje.Text = textomMensaje;
				this.Visible = true;
			}
			else
			{
				this.Visible = false;
			}
			
			
			
			switch(mensajeTipo)
			{
				case TipoError.Error:
					picIcon.Image = imageList1.Images[0];
					if(colorFondo.Name == "0")
						this.BackColor = wDefaultColorError;
					else
						this.BackColor = colorFondo;
					break;

				case TipoError.Warning:
					picIcon.Image = imageList1.Images[1];
					if(colorFondo.Name == "0")
						this.BackColor = wDefaultColorError;
					else
						this.BackColor = colorFondo;
					break;

				case TipoError.Information:
					picIcon.Image = imageList1.Images[2];
					if(colorFondo.Name == "0")
						this.BackColor = wDefaultColor;
					else
						this.BackColor = colorFondo;
					break;

				case TipoError.Question:
					picIcon.Image = imageList1.Images[3];
					if(colorFondo.Name == "0")
						this.BackColor = wDefaultColor;
					else
						this.BackColor = colorFondo;
					break;

			
				case TipoError.None:
					picIcon.Image = null;
					if(colorFondo.Name == "0")
						this.BackColor = wDefaultColor;
					else
						this.BackColor = colorFondo;
					break;
			}
		}

	}
}
