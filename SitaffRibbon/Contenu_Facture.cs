using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitaffRibbon
{
	public partial class Contenu_Facture
	{
		public string isText
		{
			get
			{
				string retour = "";

				if (this.Description != null)
				{
					if (this.Description != "")
					{
						retour = "T";
					}
				}

				return retour;
			}
		}
	}
}
