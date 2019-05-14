using System;
  using System.Collections;



namespace NU_Bewertung

{
    class Program
    {
        
                
        static void Main(string[] args)
        {
            //MAtrizendeklaration
            int[] AryKred;
            string[] AryKrit;
            double[,] AryEM;
            double[,] AryGewichtung;
            double[,] AryGewEM;
            double[,] NormAryEM;
            double[] AryKriterienGewichtung;

            
          
            //Matrizeninitalisierung

            int AnzahlKreditoren = 11;

            AryEM = new double[11, 5] {{ 2.8, 320, 56897.50, 659874, 25 },{ 3.2, 280, 1254786, 36523, 63 },{ 1.5, 410, 2365874, 12985, 210 },{ 2.3, 250, 125487, 547896.69, 123 },{ 5, 190, 563254, 32647, 23 },
                                       { 4.3,550,5398741,985621,26 },{ 3.8,230,23651.23,12365,47 },{ 3.1,170,236987,65456,196 },{ 2.9,120,69874,356741,45 },{ 1.8,260,69853,36523,26 },{ 1.2,370,63542,25874,78 }};
            AryGewichtung = new double[5, 5];
            AryKriterienGewichtung = new double[5];
            NormAryEM = new double[11, 5];

            Klassen.Kriterium Kriterium1 = new Klassen.Kriterium();
                Kriterium1.strKriteriumName = "Bewertungsnote";
                Console.WriteLine("Geben Sie die Gewichtung für Kriterium {0} ein.", Kriterium1.strKriteriumName);
                Kriterium1.dblGewichtung = Convert.ToDouble(Console.ReadLine());
                Kriterium1.strBestCase = "Min";

            Klassen.Kriterium Kriterium2 = new Klassen.Kriterium();
                Kriterium2.strKriteriumName = "Bonität";
                Console.WriteLine("Geben Sie die Gewichtung für Kriterium {0} ein.", Kriterium2.strKriteriumName);
                Kriterium2.dblGewichtung = Convert.ToDouble(Console.ReadLine());
                Kriterium2.strBestCase = "Min";

            Klassen.Kriterium Kriterium3 = new Klassen.Kriterium();
                Kriterium3.strKriteriumName = "Umsatz";
                Console.WriteLine("Geben Sie die Gewichtung für Kriterium {0} ein.", Kriterium3.strKriteriumName);
                Kriterium3.dblGewichtung = Convert.ToDouble(Console.ReadLine());
                Kriterium3.strBestCase = "Max";

            Klassen.Kriterium Kriterium4 = new Klassen.Kriterium();
                Kriterium4.strKriteriumName = "Kosten";
                Console.WriteLine("Geben Sie die Gewichtung für Kriterium {0} ein.", Kriterium4.strKriteriumName);
                Kriterium4.dblGewichtung = Convert.ToDouble(Console.ReadLine());
                Kriterium4.strBestCase = "Min";

            Klassen.Kriterium Kriterium5 = new Klassen.Kriterium();
                Kriterium5.strKriteriumName = "Fehler";
                Console.WriteLine("Geben Sie die Gewichtung für Kriterium {0} ein.", Kriterium5.strKriteriumName);
                Kriterium5.dblGewichtung = Convert.ToDouble(Console.ReadLine());
                Kriterium5.strBestCase = "Max";

            Klassen.Kriterium[] KL = new Klassen.Kriterium[5] { Kriterium1, Kriterium2, Kriterium3,Kriterium4,Kriterium5 };
                
       

            //Wertzuweisung der Gewichtungsmatrix
            for (int spalte = 0; spalte <= AryGewichtung.GetUpperBound(1); spalte++)
            {
                for (int zeile = 0; zeile <= AryGewichtung.GetUpperBound(0); zeile++)
                {
                    if (zeile == spalte)
                        AryGewichtung[zeile, spalte] = KL[spalte].dblGewichtung;
                    else
                        AryGewichtung[zeile, spalte] = 0;
                    Console.WriteLine("Gewichtungsmatrix {0},{1} = {2}", zeile, spalte, AryGewichtung[zeile, spalte]);
                }

            }
            Console.ReadKey();
           
            //Normalisieurng der Entscheidungsmatrix

            //Console.WriteLine();
            for (int spalte = 0; spalte <= AryEM.GetUpperBound(1); spalte++)
            {
                double summe = 0;
                for (int zeile = 0; zeile <= AryEM.GetUpperBound(0); zeile++)
                {
                           
                    summe += Math.Pow(AryEM[zeile, spalte], 2);
                
                }

                summe = Math.Sqrt(summe);
                                
                for (int zeile = 0; zeile <= AryEM.GetUpperBound(0); zeile++)
                {
                    
                    NormAryEM[zeile, spalte] = AryEM[zeile, spalte] / summe;
                    Console.WriteLine("Normalisierte Entscheidungsmatrix {0}, {1} = {2} ",zeile,spalte,NormAryEM[zeile, spalte]);                      

                }

            }
        
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine();

            //Berechnung der gewichteten Normalisieurngsmatrix

            AryGewEM = new double[NormAryEM.GetUpperBound(0) + 1, AryGewichtung.GetUpperBound(1) + 1];

            for (int zeile = 0; zeile<=NormAryEM.GetUpperBound(0); zeile++)
            {
                for (int spalte = 0; spalte<=AryGewichtung.GetUpperBound(1); spalte++)
                {
                    AryGewEM[zeile,spalte ] = 0;
                    for (int i = 0; i <= NormAryEM.GetUpperBound(1); i++) AryGewEM[zeile, spalte] = AryGewEM[zeile,spalte]+ NormAryEM[zeile, i] * AryGewichtung[i, spalte];

                    Console.WriteLine("Gewichtete normalisiete Entscheidungsmatrix {0},{1} = {2}", zeile,spalte,AryGewEM[zeile, spalte]);
                
                }
            }
          
            Console.ReadLine();
            //Ableitung virtueller Alternativen

            double[] BestCase = new double[AryGewEM.GetUpperBound(1)+1];
            double[] WorstCase = new double[AryGewEM.GetUpperBound(1)+1];



            for (int spalte = 0; spalte <= AryGewEM.GetUpperBound(1); spalte++)
            {

                BestCase[spalte] = AryGewEM[0, spalte];
                WorstCase[spalte] = AryGewEM[0, spalte];
                string Wert = KL[spalte].strBestCase;
                
                for (int zeile = 0; zeile <= AryGewEM.GetUpperBound(0); zeile++)
                {
                    switch(Wert)
                    {
                        case "Min":
                            if (AryGewEM[zeile, spalte] < BestCase[spalte])
                                BestCase[spalte] = AryGewEM[zeile, spalte];
                            else if (AryGewEM[zeile, spalte] > WorstCase[spalte])
                                WorstCase[spalte] = AryGewEM[zeile, spalte];
                            break;
                        case "Max":
                            if (AryGewEM[zeile, spalte] > BestCase[spalte])
                                BestCase[spalte] = AryGewEM[zeile, spalte];
                            else if (AryGewEM[zeile, spalte] < WorstCase[spalte])
                                WorstCase[spalte] = AryGewEM[zeile, spalte];
                            break;

                    }
                }
              
            }
         
            Console.WriteLine("BestCase {1} = {0}",BestCase[0], KL[0].strKriteriumName);
            Console.WriteLine("BestCase {1} = {0}",BestCase[1],KL[1].strKriteriumName);//
            Console.WriteLine("BestCase {1} = {0}", BestCase[2], KL[2].strKriteriumName);
            Console.WriteLine("BestCase {1} = {0}", BestCase[3], KL[3].strKriteriumName);
            Console.WriteLine("BestCase {1} = {0}", BestCase[4], KL[4].strKriteriumName);

            Console.WriteLine("WorstCase {1} = {0}",WorstCase[0], KL[0].strKriteriumName);
            Console.WriteLine("WorstCase {1} = {0}", WorstCase[1], KL[1].strKriteriumName);
            Console.WriteLine("WorstCase {1} = {0}", WorstCase[2], KL[2].strKriteriumName);
            Console.WriteLine("WorstCase {1} = {0}", WorstCase[3], KL[3].strKriteriumName);
            Console.WriteLine("WorstCase {1} = {0}", WorstCase[4], KL[4].strKriteriumName);
            

            Console.ReadLine();

            //Berechnung des Abstandsmaß

            double[,] AryAbstaende;
            AryAbstaende = new double[AryGewEM.GetUpperBound(0)+1, 2];

            for (int zeile = 0; zeile <= AryGewEM.GetUpperBound(0); zeile++)
            {
                double AbstandWC = 0;
                double AbstandBC = 0;

                for (int spalte = 0; spalte <= AryGewEM.GetUpperBound(1); spalte++)
                {
                    AbstandWC += Math.Pow((AryGewEM[zeile, spalte] - WorstCase[spalte]), 2);
                    AbstandBC += Math.Pow((AryGewEM[zeile, spalte] - BestCase[spalte]), 2);
                }
                AryAbstaende[zeile, 0] = Math.Sqrt(AbstandBC);
                AryAbstaende[zeile, 1] = Math.Sqrt(AbstandWC);
                Console.WriteLine("Abstand BC DMU {1} = {0}", AryAbstaende[zeile, 0],zeile);
                Console.WriteLine("Abstand WC DMU {1} = {0}", AryAbstaende[zeile, 1], zeile);
            }
            Console.ReadKey();
            Console.WriteLine();
            // Bestimmung der relativen Nähe zur BC-Alternative
            double [] Effizienzindex;

            Effizienzindex = new double[AryGewEM.GetUpperBound(0)+1];

            for (int zeile = 0; zeile <= AryGewEM.GetUpperBound(0); zeile++)
            {
                Effizienzindex[zeile] = AryAbstaende[zeile, 1] / (AryAbstaende[zeile, 0] + AryAbstaende[zeile, 1]);
                //Effiziensindex an Kredtitorobjekt übergeben
                Console.WriteLine("Effienzindex DMU {0} = {1}", zeile, Effizienzindex[zeile]);
            }
            Console.ReadLine();
        }   
    }
}

         
