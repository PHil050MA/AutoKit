using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMCMotionSDK;
using FileHelper;
namespace mainfrm
{
    public class TeachData
    {
        public enum eList
        {
            LoadPos,
            pos1_1, pos1_2, pos1_3, pos1_4, pos1_5, pos1_6, pos1_7,
            pos2_1, pos2_2, pos2_3, pos2_4, pos2_5, pos2_6, pos2_7,
            pos3_1, pos3_2, pos3_3, pos3_4, pos3_5, pos3_6, pos3_7, Max
        }
        public enum eListGroup1
        {
            LoadPos,
            pos1_1, pos2_1, pos3_1,
            pos1_2, pos2_2, pos3_2,
            pos1_3, pos2_3, pos3_3, Max
        }
        public enum eListGroup2
        {
            LoadPos,
            pos1_7, pos2_7, pos3_7,
            pos1_6, pos2_6, pos3_6,
            pos1_5, pos2_5, pos3_5, Max
        }

        public enum eLoadList
        {
            loadPos,
        }
        public TeachData(string filePath)
        {
            FileHelper.FileHelper.fileFullPath = filePath;
            teachData = new Dictionary<eList, DPoint>();
            Load();
        }
        Dictionary<eList, DPoint> teachData;
        //240903 개별 티칭 구간 추가
        Dictionary<eListGroup1, DPoint> teachDataGroup1;
        Dictionary<eListGroup2, DPoint> teachDataGroup2;

        public DPoint this[eList idx]
        {
            get => teachData[idx];
            set => teachData[idx] = value;
        }
        // Group 1의 인덱서를 정의합니다.
        public DPoint this[eListGroup1 idx]
        {
            get => teachDataGroup1[idx];
            set => teachDataGroup1[idx] = value;
        }
        // Group 2의 인덱서를 정의합니다.
        public DPoint this[eListGroup2 idx]
        {
            get => teachDataGroup2[idx];
            set => teachDataGroup2[idx] = value;
        }

        public void Load()
        {
            Dictionary<string, string> dic = FileHelper.FileHelper.FileLoad();
            for ( eList ekey = 0; ekey < eList.Max; ekey++ )
            {
                string key = ekey.ToString();
                DPoint value = new DPoint(0, 0);
                try
                {
                    if ( dic.ContainsKey(key) )
                    {
                        string[] strs = dic[key].Split(',');
                        value.X = double.Parse(strs[0]);
                        value.Y = double.Parse(strs[1]);
                    }
                }
                catch ( Exception )
                {
                    value.SetValue(0, 0);
                }
                if ( teachData.ContainsKey(ekey) ) teachData[ekey] = value;
                else teachData.Add(ekey, value);

                Save();
            }
            // Group 1 데이터를 로드합니다.
            //for (eListGroup1 ekey = 0; ekey < eListGroup1.Max; ekey++) {
            //    LoadData(teachDataGroup1, ekey, dic);
            //}
            //// Group 2 데이터를 로드합니다.
            //for (eListGroup2 ekey = 0; ekey < eListGroup2.Max; ekey++) {
            //    LoadData(teachDataGroup2, ekey, dic);
            //}
            //save는 위에서 같은 데이터 진행하기때문에 X
        }

        //private void LoadData<T>(Dictionary<T, DPoint> dictionary, T ekey, Dictionary<string, string> dic) {
        //    string key = ekey.ToString();
        //    DPoint value = new DPoint(0, 0);
        //    try {
        //        if (dic.ContainsKey(key)) {
        //            string[] strs = dic[key].Split(',');
        //            value.X = double.Parse(strs[0]);
        //            value.Y = double.Parse(strs[1]);
        //        }
        //    }
        //    catch (Exception) {
        //        value.SetValue(0, 0);
        //    }

        //    if (dictionary.ContainsKey(ekey))
        //        dictionary[ekey] = value;
        //    else
        //        dictionary.Add(ekey, value);
        //}
        public void Save()
        {
            //해야 되는거
            FileHelper.FileHelper.fileFullPath = Global.TeachingFileSavePath + "Teach.txt";
            FileHelper.FileHelper.FileDictionarySave(teachData);
            //FileDictionarySave(param);
        }
    }
}

