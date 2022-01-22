using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IoMaker
{
    public class ioMakerLogic
    {
        public List<string> io_list = new List<string>();
        public List<string> inv_list = new List<string>();
        public List<string> access_list = new List<string>();
        public List<string> var_list = new List<string>();
        public List<string> all_list = new List<string>();
        public struct acVariable{
            public acVariable(string iname, string iadress, string itype){
                name = iname;
                adress = iadress;
                type = itype;
            }
            public string name {get; set;}
            public string adress{get; set;}
            public string type{get; set;}
            public override string ToString()
            {
                return ($"Variable name: {name}, adress: {adress}, type: {type}");
            }
        }
        public void makeGeneral(string Data) {
            string re_string = @"^(.*) AT (.*)\s?:\s?(.*)\s?;";
            // Reset lists
            io_list = new List<string>();
            inv_list = new List<string>();
            access_list = new List<string>();
            var_list = new List<string>();
            all_list = new List<string>();
            foreach (string row in Data.Split("\n"))
            {

                Match res = Regex.Match(row, re_string);
                acVariable vv = new acVariable();
                vv.name = res.Groups[1].Value;
                vv.adress = res.Groups[2].Value;
                vv.type = res.Groups[3].Value;

                vv.name = vv.name.Trim();
                vv.adress = vv.adress.Trim();
                vv.type = vv.type.Trim();
                
                System.Console.WriteLine(vv);

                if (vv.type == "INT") {
                    if (vv.adress.Contains("IW")) {
                        makeAI(vv.name);
                    }
                    if (vv.adress.Contains("QW")) {
                        makeAO(vv.name);
                    }
                } else if (vv.type == "BOOL") {
                    if (vv.adress.Contains("IX")){
                        makeDI(vv.name);
                    }
                    if (vv.adress.Contains("QX")){
                        makeDO(vv.name);
                    }
                }
            }
        }

        public void makeAO(string Data){
            string tag = Data;
            tag = tag.Replace("_IO", "");

            string io_string = @$"
                fb{tag}(
                In:={tag} ,
                In_Man := {tag}_rMAN,
                Man := {tag}_MMAN,
                Skala_Min := {tag}_Min,
                Skala_Max := {tag}_Max,
                Decimaler := 2,
                IO =>{tag}_IO ,
                uUT=>{tag}_VVAR ); \r\n
            ";
            io_list.Add(io_string);
            all_list.Add(io_string);

            string inv_string = @$"
            fb{tag} : AO_Skal;

            {tag}_Min : REAL;
            {tag}_Max : REAL;
            ";
            inv_list.Add(inv_string);
            all_list.Add(io_string);

            string access_string = @$"
            {tag}_rMAN : UINT;
            {tag}_VVAR : UINT;

            {tag}_MMAN : BOOL;
            ";
            access_list.Add(access_string);
            all_list.Add(access_string);

            var_list.Add($"{tag} : REAL;\r\n");
            all_list.Add($"{tag} : REAL;\r\n");
        }
        public void makeDO(string Data){
            string tag = Data;
            tag = tag.Replace("_IO", "");
            string io_string = $"{tag}_IO := {tag};\r\n";
            io_list.Add(io_string);
            all_list.Add(io_string);
            string access_string = $"{tag} : BOOL;\r\n";
            access_list.Add(access_string);
            all_list.Add(access_string);
        }
        public void makeDI(string Data) {
            string tag = Data;
            tag = tag.Replace("_IO", "");
            string io_string = $"{tag} := DI({tag}_IO, {tag}_INV, FALSE);\r\n";
            io_list.Add(io_string);
            all_list.Add(io_string);

            string inv_string = $"{tag}_INV : BOOL;\r\n";
            inv_list.Add(inv_string);
            all_list.Add(inv_string);

            string access_string = $"{tag} : BOOL;\r\n";
            access_list.Add(access_string);
            all_list.Add(access_string);
        }

        public void makeAI(string Data) {
            string tag = Data;
            tag = tag.Replace("_IO", "");

            string io_string = @$"
                fb{tag}(
                In:={tag}_IO ,
                Skala_Min:={tag}_Min ,
                Skala_Max:={tag}_Max ,
                LGH:=utor({tag}_GHOG, fb{tag}.DECIMALER) ,
                LGL:= utor({tag}_GLAG, fb{tag}.DECIMALER) ,
                Tid:= 60,
                BLOCK:= FALSE,
                HYSTERES:=0 ,
                DECIMALER:= 2,
                rUT=>{tag} ,
                uUT=>{tag}_VVAR ,
                HL=>{tag}_LHOG ,
                LL=>{tag}_LLAG ,
                FEL=>{tag}_LSIG ); \r\n
            ";
            io_list.Add(io_string);
            all_list.Add(io_string);

            string inv_string = @$"
            fb{tag} : AI_REAL;

            {tag}_Min : REAL;
            {tag}_Max : REAL;
            ";
            inv_list.Add(inv_string);
            all_list.Add(inv_string);

            string access_string = @$"
            {tag}_GHOG : UINT;
            {tag}_GLAG : UINT;
            {tag}_VVAR : UINT;

            {tag}_LSIG : BOOL;
            {tag}_LLAG : BOOL;
            {tag}_LHOG : BOOL;
            ";
            access_list.Add(access_string);
            all_list.Add(access_string);

            var_list.Add($"{tag} : REAL;\r\n");
            all_list.Add($"{tag} : REAL;\r\n");
        }
        public void makeDI_old(string Data) {
            io_list = new List<string>();
            inv_list = new List<string>();
            access_list = new List<string>();
            var_list = new List<string>();
            foreach (string row in Data.Split('\n'))
            {
                string tag = row.Split()[0];
                //tag = i.split()[0]
                tag = tag.Replace("_IO", "");
                //tag = tag.replace("_IO", "")
                string io_string = $"{tag} := DI({tag}_IO, {tag}_INV, FALSE);\r\n";
                //io_string = f"{tag} := DI({tag}_IO, {tag}_INV, FALSE);\r\n"
                io_list.Add(io_string);
                string inv_string = $"{tag}_INV : BOOL;\r\n";
                inv_list.Add(inv_string);
                string access_string = $"{tag} : BOOL;\r\n";
                access_list.Add(access_string);
            }
        }
        public void makeAI_old(string Data) {
            io_list = new List<string>();
            inv_list = new List<string>();
            access_list = new List<string>();
            var_list = new List<string>();
            foreach (string row in Data.Split('\n')) {
                string tag = row.Split(" ")[0];
                tag = tag.Replace("_IO", "");

                string io_string = @$"
                    fb{tag}(
                    In:={tag}_IO ,
                    Skala_Min:={tag}_Min ,
                    Skala_Max:={tag}_Max ,
                    LGH:=utor({tag}_GHOG, fb{tag}.DECIMALER) ,
                    LGL:= utor({tag}_GLAG, fb{tag}.DECIMALER) ,
                    Tid:= 60,
                    BLOCK:= FALSE,
                    HYSTERES:=0 ,
                    DECIMALER:= 2,
                    rUT=>{tag} ,
                    uUT=>{tag}_VVAR ,
                    HL=>{tag}_LHOG ,
                    LL=>{tag}_LLAG ,
                    FEL=>{tag}_LSIG ); \r\n
                ";
                io_list.Add(io_string);

                string inv_string = @$"
                fb{tag} : AI_REAL;

                {tag}_Min : REAL;
                {tag}_Max : REAL;
                ";
                inv_list.Add(inv_string);

                string access_string = @$"
                {tag}_GHOG : UINT;
                {tag}_GLAG : UINT;
                {tag}_VVAR : UINT;

                {tag}_LSIG : BOOL;
                {tag}_LLAG : BOOL;
                {tag}_LHOG : BOOL;
                ";
                access_list.Add(access_string);
                var_list.Add($"{tag} : REAL;\r\n");
            }
        }
    }
}
