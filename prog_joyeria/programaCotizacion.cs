using System;
using System.Windows.Forms;

namespace prog_joyeria
{
    partial class Form1 : Form
    {
        private void Cotizacion()
        {

            try
            {
                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.kitco.com/market/");

                HtmlAgilityPack.HtmlWeb web2 = new HtmlAgilityPack.HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc2 = web2.Load("https://cuantoestaeldolar.pe/cambio-de-dolar-online");
            
                

                //Calculo de Precio de compra de Oro 24k, 18k, 14k
                string scrapOroOz = doc.DocumentNode.SelectNodes("//td[@id='AU-bid']")[0].InnerText;
                double OroG = double.Parse(scrapOroOz) / 31.1;
                double OroGramos7porc = Math.Round(OroG * 0.93, 2);
                double OroGramos71porc = Math.Round(OroGramos7porc * 0.71, 2);
                double OroGramos50porc = Math.Round(OroGramos7porc * 0.5, 2);

                //Calculo de Precio de Venta de Oro 24k, 18k
                string scrapOroOzV = doc.DocumentNode.SelectNodes("//td[@id='AU-ask']")[0].InnerText;
                double OroG24KV = double.Parse(scrapOroOzV) / 31.1;
                double OroG18KV = OroG24KV * 0.75;

                //Calculo de Precio de compra de Plata Piña, 925, 800
                string scrapPlataOz = doc.DocumentNode.SelectNodes("//td[@id='AG-bid']")[0].InnerText;
                double PlataG = double.Parse(scrapPlataOz) / 31.1;
                double Plata20porc = Math.Round(PlataG * 0.8, 2);
                double Plata25porc = Math.Round(Plata20porc * 0.75, 2);
                double Plata30porc = Math.Round(Plata25porc * 0.7, 2);

                //Calculo de Precio de venta de Plata Piña
                string scrapPlataOzV = doc.DocumentNode.SelectNodes("//td[@id='AG-ask']")[0].InnerText;
                double PlataGV = double.Parse(scrapPlataOzV) / 31.1;

                //Calculo de Precio de compra de Platino
                string scrapPlatinoOz = doc.DocumentNode.SelectNodes("//td[@id='PT-bid']")[0].InnerText;
                double PlatinoG = Math.Round(double.Parse(scrapPlatinoOz) / 31.1, 2);

                //Calculo de Precio de venta de Platino
                string scrapPlatinoOzV = doc.DocumentNode.SelectNodes("//td[@id='PT-ask']")[0].InnerText;
                double PlatinoGV = Math.Round(double.Parse(scrapPlatinoOzV) / 31.1, 2);


                //Calculo de Precio de compra de Paladio
                string scrapPaladioOz = doc.DocumentNode.SelectNodes("//td[@id='PD-bid']")[0].InnerText;
                double PaladioG = Math.Round(double.Parse(scrapPaladioOz) / 31.1, 2);

                //Calculo de Precio de venta de Paladio
                string scrapPaladioOzV = doc.DocumentNode.SelectNodes("//td[@id='PD-ask']")[0].InnerText;
                double PaladioGV = Math.Round(double.Parse(scrapPaladioOzV) / 31.1, 2);


                //Calculo de Precio de compra de Rodio
                string scrapRodioOz = doc.DocumentNode.SelectNodes("//td[@id='RH-bid']")[0].InnerText;
                double RodioG = Math.Round(double.Parse(scrapRodioOz) / 31.1, 2);

                //Calculo de Precio de venta de Rodio
                string scrapRodioOzV = doc.DocumentNode.SelectNodes("//td[@id='RH-ask']")[0].InnerText;
                double RodioGV = Math.Round(double.Parse(scrapRodioOzV) / 31.1, 2);

                //Calculo de Precio de venta de Oro Blanco
                double OroBlancoGV = Math.Round(OroG18KV + 0.09 * PlataGV + 0.16 * PaladioGV, 2);


                //Calculo de Precio de compra venta dolar a soles sunat
                string DolarCompraSnt = doc2.DocumentNode.SelectNodes("//div[@class='td tb_dollar_compra']//text()[normalize-space()]")[2].InnerText;
                double DolarCompraSunat = double.Parse(DolarCompraSnt);
                scrapDolarCompra.Text = DolarCompraSnt;

                string DolarVentaSnt = doc2.DocumentNode.SelectNodes("//div[@class='td tb_dollar_venta']//text()[normalize-space()]")[2].InnerText;
                double DolarVentaSunat = double.Parse(DolarVentaSnt);
                scrapDolarVenta.Text = DolarVentaSnt;

                //Calculo de Precio de compra venta dolar a soles paralelo
                string DolarCompraPrl = doc2.DocumentNode.SelectNodes("//div[@class='td tb_dollar_compra']//text()[normalize-space()]")[4].InnerText;
                double DolarCompraParalelo = double.Parse(DolarCompraPrl);
                scrapDolarCompraPrl.Text = DolarCompraPrl;

                string DolarVentaPrl = doc2.DocumentNode.SelectNodes("//div[@class='td tb_dollar_venta']//text()[normalize-space()]")[4].InnerText;
                double DolarVentaParalelo = double.Parse(DolarVentaPrl);
                scrapDolarVentaPrl.Text = DolarVentaPrl;


                if (tabCotizacioncbMoneda.Text == "USD")
                {
                    scrapOroFino.Text = OroGramos7porc.ToString();
                    scrapOro18k.Text = OroGramos71porc.ToString();
                    scrapOro14k.Text = OroGramos50porc.ToString();

                    scrapOroFinoV.Text = Math.Round(OroG24KV, 2).ToString();
                    scrapOro18kV.Text = Math.Round(OroG18KV, 2).ToString();

                    scrapPlataFina.Text = Plata20porc.ToString();
                    scrapPlata925.Text = Plata25porc.ToString();
                    scrapPlata800.Text = Plata30porc.ToString();

                    scrapPlataFinaV.Text = Math.Round(PlataGV, 2).ToString();


                    scrapPlatinoFinoV.Text = PlatinoGV.ToString();

                    scrapPaladioFinoV.Text = PaladioGV.ToString();

                    scrapRodioFinoV.Text = RodioGV.ToString();

                    scrapOroBlanco18kV.Text = OroBlancoGV.ToString();

                }
                else
                {

                    OroGramos7porc = Math.Round(OroGramos7porc * DolarVentaSunat, 1);
                    OroGramos71porc = Math.Round(OroGramos71porc * DolarVentaSunat, 1);
                    OroGramos50porc = Math.Round(OroGramos50porc * DolarVentaSunat, 1);
                    OroG24KV = Math.Round(OroG24KV * DolarVentaSunat, 1);
                    OroG18KV = Math.Round(OroG18KV * DolarVentaSunat, 1);

                    Plata20porc = Math.Round(Plata20porc * DolarVentaSunat, 1);
                    Plata25porc = Math.Round(Plata25porc * DolarVentaSunat, 1);
                    Plata30porc = Math.Round(Plata30porc * DolarVentaSunat, 1);

                    PlataGV = Math.Round(PlataGV * DolarVentaSunat, 1);

                    PlatinoGV = Math.Round(PlatinoGV * DolarVentaSunat, 1);
                    PaladioGV = Math.Round(PaladioGV * DolarVentaSunat, 1);
                    RodioGV = Math.Round(RodioGV * DolarVentaSunat, 1);

                    OroBlancoGV = Math.Round(OroBlancoGV * DolarVentaSunat, 1);


                    scrapOroFino.Text = OroGramos7porc.ToString();
                    scrapOro18k.Text = OroGramos71porc.ToString();
                    scrapOro14k.Text = OroGramos50porc.ToString();

                    scrapOroFinoV.Text = Math.Round(OroG24KV, 2).ToString();
                    scrapOro18kV.Text = Math.Round(OroG18KV, 2).ToString();

                    scrapPlataFina.Text = Plata20porc.ToString();
                    scrapPlata925.Text = Plata25porc.ToString();
                    scrapPlata800.Text = Plata30porc.ToString();


                    scrapPlataFinaV.Text = Math.Round(PlataGV, 2).ToString();

                    scrapPlatinoFinoV.Text = PlatinoGV.ToString();

                    scrapPaladioFinoV.Text = PaladioGV.ToString();

                    scrapRodioFinoV.Text = RodioGV.ToString();

                    scrapOroBlanco18kV.Text = OroBlancoGV.ToString();
                }

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message+": No hay internet o la página del servidor está caído", "ERROR");
            }
        }
    }
}
