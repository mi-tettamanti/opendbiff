using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DBDiff.Schema.SQLServer.Model;

namespace DBDiff.Schema.SQLServer.Compare
{
    internal class CompareXMLSchemas:CompareBase<XMLSchema>
    {
        public static void GenerateDiferences(XMLSchemas CamposOrigen, XMLSchemas CamposDestino)
        {
            foreach (XMLSchema node in CamposDestino)
            {
                if (!CamposOrigen.Exists(node.FullName))
                {
                    node.Status = Enums.ObjectStatusType.CreateStatus;
                    CamposOrigen.Add(node);
                }
                else
                {
                    if (!XMLSchema.Compare(node, CamposOrigen[node.FullName]))
                    {
                        XMLSchema newNode = node.Clone(CamposOrigen.Parent);
                        newNode.Status = Enums.ObjectStatusType.AlterStatus;
                        CamposOrigen[node.FullName] = newNode;
                    }
                }
            }

            MarkDrop(CamposOrigen, CamposDestino);
        }
    }
}