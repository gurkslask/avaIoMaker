using System.Collections.Generic;
using System.Xml;
namespace IoMaker
{
        public class Templates
        {
                public struct tDI{
                    public string inv_string;
                    public string io_string;
                    public string access_string;
                    public string var_string;
                }
                public struct tAI{
                    public string inv_string;
                    public string io_string;
                    public string access_string;
                    public string var_string;
                }
                public struct tDO{
                    public string inv_string;
                    public string io_string;
                    public string access_string;
                    public string var_string;
                }
                public struct tAO{
                    public string inv_string;
                    public string io_string;
                    public string access_string;
                    public string var_string;
                }
                public struct ioType{
                    public ioType(){
                        DI = new tDI();
                        AI = new tAI();
                        DO = new tDO();
                        AO = new tAO();
                    }
                    public tDI DI;
                    public tAI AI;
                    public tAO AO;
                    public tDO DO;
                }
                public Dictionary<string, ioType> temps = new Dictionary<string, ioType>();
                public static Dictionary<string, ioType> loadTemplate(string name){
                    Dictionary<string, ioType> tdict = new Dictionary<string, ioType>();
                    string path = "./template.XML";
                    XmlReader xr = XmlReader.Create(path);
                    xr.MoveToFirstAttribute();
                    Console.WriteLine($"{ xr.GetAttribute("io_string")}");
                    return tdict;
                }

                public static Dictionary<string, ioType> initDefaultTemplate(){
                    // Not finished !
                    Dictionary<string, ioType> tdict = new Dictionary<string, ioType>();
                    ioType tt = new ioType();

                    tt.DI.io_string = @"
                    (*{1}*)
                    {0} := DI({0}_IO, {0}_INV, FALSE);";
                    tt.DI.inv_string = "{0}_INV : BOOL;\r\n";
                    tt.DI.access_string = "{0} : BOOL;\r\n";

                    tt.DO.io_string = @"
                    (*{1}*)
                    {0}_IO := {0};";
                    tt.DO.access_string = "{0} : BOOL;\r\n";

                    tt.AO.io_string = @"
                        (*{1}*)
                        fb{0}(
                        In:={0} ,
                        In_Man := {0}_rMAN,
                        Man := {0}_MMAN,
                        Skala_Min := {0}_Min,
                        Skala_Max := {0}_Max,
                        Decimaler := 2,
                        IO =>{0}_IO ,
                        uUT=>{0}_VVAR ); 
                    ";
                    tt.AO.inv_string = @"
                    fb{0} : AO_Skal;

                    {0}_Min : REAL;
                    {0}_Max : REAL;
                    ";
                    tt.AO.access_string = @"
                    {0}_rMAN : UINT;
                    {0}_VVAR : UINT;

                    {0}_MMAN : BOOL;
                    ";
                    tt.AO.var_string = "{0} : REAL;\r\n";


                    tt.AI.io_string = @"
                        (*{1}*)
                        fb{0}(
                        In:={0}_IO ,
                        Skala_Min:={0}_Min ,
                        Skala_Max:={0}_Max ,
                        LGH:=utor({0}_GHOG, fb{0}.DECIMALER) ,
                        LGL:= utor({0}_GLAG, fb{0}.DECIMALER) ,
                        Tid:= 60,
                        BLOCK:= FALSE,
                        HYSTERES:=0 ,
                        DECIMALER:= 2,
                        rUT=>{0} ,
                        uUT=>{0}_VVAR ,
                        HL=>{0}_LHOG ,
                        LL=>{0}_LLAG ,
                        FEL=>{0}_LSIG ); 
                    ";
                    tt.AI.inv_string = @"
                    fb{0} : AI_REAL;

                    {0}_Min : REAL;
                    {0}_Max : REAL;
                    ";
                    tt.AI.access_string = @"
                    {0}_GHOG : UINT;
                    {0}_GLAG : UINT;
                    {0}_VVAR : UINT;

                    {0}_LSIG : BOOL;
                    {0}_LLAG : BOOL;
                    {0}_LHOG : BOOL;
                    ";

                    tt.AI.var_string = "{0} : REAL;\r\n";

                    tdict.Add("Default", tt);
                    return tdict;
                }
        }
}

