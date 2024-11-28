using System.Collections.Generic;
namespace mainfrm
{
    public class ServoParam
    {
        public ServoParam(string fileName)
        {
            FileHelper.FileHelper.fileFullPath = fileName;
            param = new Dictionary<eList, double>();
            Load();
        }
        public enum eList
        {
            JogVel, JogAcc, JogDec, JogJerk,
            PosVel, PosAcc, PosDec, PosJerk,
            InchVel, InchAcc, InchDec, InchJerk,
            Max
        }
        Dictionary<eList, double> param;
        public double this[eList idx]
        {
            get { return param[idx]; }
            set { param[idx] = value; }
        }
        public void Load()
        {
            Dictionary<string, string> dic = FileHelper.FileHelper.FileLoad();
            for (eList ekey = 0; ekey < eList.Max; ekey++)
            {
                string key = ekey.ToString();
                double value = 0.0;
                if (dic.ContainsKey(key))
                {
                    double.TryParse(dic[key], out value);
                }
                else
                {
                    if (key.Contains("Dec") || key.Contains("Acc")) value = 100000.0;
                    else if (key.Contains("Vel")) value = 50000.0;
                    else value = 100000.0;
                }
                if (param.ContainsKey(ekey)) param[ekey] = value;
                else param.Add(ekey, value);

                Save();
            }
        }
        //안되서 해결해야됨.
        public void Save()
        {
            FileHelper.FileHelper.FileDictionarySave(param);
            //FileHelper.FileSave<eList,string>( , filepath)
        }
    }
}
