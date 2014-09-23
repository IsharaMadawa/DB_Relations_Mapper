using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DB_Connector
{
    public class RelationsMapper
    {
        public ArrayList MergeJoins(ArrayList list1)
        {
            ArrayList list = new ArrayList(list1);
            bool flag = false;
            for (int k = 0; k < list.Count - 1; k++)
            {
                for (int i = 0; i < (list.Count + 1); i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        List<string> newList = new List<string>(MergeElement((List<string>)list[j], (List<string>)list[i], out flag)); 
                        if (flag == true)
                        {
                            list.RemoveAt(j);
                            list[i] = newList;
                            --j;
                        }
                    }
                    if (flag == true && list.Count == 2)
                    {
                        i = -1;
                    }
                }
            }
            return list;
        }
        private List<string> MergeElement(List<string> subList, List<string> mainList, out bool flag)
        {
            flag = false;
            List<string> newList1 = new List<string>(subList);
            foreach (string item in subList)
            {
                if (mainList.Contains(item))
                {
                    flag = true;
                    newList1.Remove(item);
                }
            }
            if (flag == true)
            {
                mainList.AddRange(newList1);
                return mainList;
            }
            else
            {
                return mainList;
            }
        }
    }
}
