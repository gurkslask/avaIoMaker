using System.Text.RegularExpressions;
namespace IoMaker
{
  public class ioMakerLogicAB
  {
    public List<string> io_list = new List<string>();
    public List<string> inv_list = new List<string>();
    public List<string> access_list = new List<string>();
    public List<string> var_list = new List<string>();
    public List<string> all_list = new List<string>();
    public Dictionary<string, Templates.ioType> templates = Templates.initDefaultTemplate();

    public struct acVariable{
      public string name {get; set;}
      public string adress{get; set;}
      public string type{get; set;}
      public string comment{get;set;}
      public string iocard{get;set;}
      public string iodescription{get;set;}
      public override string ToString()
      {
        return ($"Variable name: {name}, adress: {adress}, type: {type}, comment: {comment}");
      }

    }
    public void makeGeneral(string Data) {
      string re_string = @"^(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)\t(.*)";
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
        vv.iocard = res.Groups[1].Value.Trim();
        vv.name = res.Groups[2].Value.Trim();
        vv.iodescription = res.Groups[3].Value.Trim();
        vv.adress = res.Groups[4].Value.Trim();
        vv.type = res.Groups[5].Value.Trim();
        vv.comment = res.Groups[6].Value.Trim();

        System.Console.WriteLine(vv);


        if (vv.type == "INT") {
          if (vv.adress.Contains("IW")) {
            makeAI(vv, "Default");
          }
          if (vv.adress.Contains("QW")) {
            makeAO(vv, "Default");
          }
        } else if (vv.type == "BOOL") {
          if (vv.adress.Contains("IX")){
            makeDI(vv, "Default");
          }
          if (vv.adress.Contains("QX")){
            makeDO(vv, "Default");
          }
        }
      }
    }
      public void makeAO(acVariable Data, string stemplate){
        Data.name = Data.name.Replace("_IO", "");
        
        string io_string = String.Format(templates[stemplate].AO.io_string, Data.name, Data.comment);
        io_list.Add(io_string);

        string inv_string = String.Format(templates[stemplate].AO.inv_string, Data.name);
        inv_list.Add(inv_string);

        string access_string = String.Format(templates[stemplate].AO.access_string, Data.name);
        access_list.Add(access_string);

        var_list.Add(String.Format(templates[stemplate].AO.var_string, Data.name));
      }
      public void makeAI(acVariable Data, string stemplate){
        Data.name = Data.name.Replace("_IO", "");
        
        string io_string = String.Format(templates[stemplate].AI.io_string, Data.name, Data.comment);
        io_list.Add(io_string);

        string inv_string = String.Format(templates[stemplate].AI.inv_string, Data.name);
        inv_list.Add(inv_string);

        string access_string = String.Format(templates[stemplate].AI.access_string, Data.name);
        access_list.Add(access_string);

        var_list.Add(String.Format(templates[stemplate].AI.var_string, Data.name));
      }
      public void makeDI(acVariable Data, string stemplate){
        Data.name = Data.name.Replace("_IO", "");
        
        string io_string = String.Format(templates[stemplate].DI.io_string, Data.name, Data.comment);
        io_list.Add(io_string);

        string inv_string = String.Format(templates[stemplate].DI.inv_string, Data.name);
        inv_list.Add(inv_string);

        string access_string = String.Format(templates[stemplate].DI.access_string, Data.name);
        access_list.Add(access_string);
      }
      public void makeDO(acVariable Data, string stemplate){
        Data.name = Data.name.Replace("_IO", "");
        
        string io_string = String.Format(templates[stemplate].DO.io_string, Data.name, Data.comment);
        io_list.Add(io_string);

        string access_string = String.Format(templates[stemplate].DO.access_string, Data.name);
        access_list.Add(access_string);
      }
      public void initDB(){databaseLogic.CreateTable();}
    }
  }
