﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebpartFile IO
    /// </summary>
    internal class ShoveWebPartFile
    {
        private long SiteID;
        private string PageName;
        private string FileName;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="pageName"></param>
        public ShoveWebPartFile(long siteID, string pageName)
        {
            SiteID = siteID;
            PageName = pageName;

            string Path = System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            if (!String.IsNullOrEmpty(PageName))
            {
                FileName = Path + "/" + PageName + ".aspx";
            }
        }

        private string BuildFile(string ContentParts, string RegisterUserControlTagName)
        {
            StringBuilder Content = new StringBuilder();

            string Inherits = System.Web.Configuration.WebConfigurationManager.AppSettings["SiteCachePageInheritsCassName"];

            if (String.IsNullOrEmpty(Inherits))
            {
                Inherits = "Shove.Web.UI.ShoveWebPartBasePageRun, Shove.Web.UI.4 For.NET 2.0";
            }

            Content.AppendLine("<%@ page language=\"C#\" autoeventwireup=\"true\" inherits=\"" + Inherits + "\" enableEventValidation=\"false\" %>");
            if (!String.IsNullOrEmpty(RegisterUserControlTagName))
            {
                Content.AppendLine(RegisterUserControlTagName);
            }
            Content.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            Content.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            Content.AppendLine("<head runat=\"server\">");
            Content.AppendLine("\t<title></title>");
            //Content.AppendLine("\t<meta name=\"keywords\" content=\"\" />"); // 留给程序的基页增加
            Content.AppendLine("\t<link href=\"ShoveWebUI_client/Style/" + SiteID.ToString() + ".css\" type=\"text/css\" rel=\"stylesheet\" />");
            Content.AppendLine("</head>");
            Content.AppendLine("<body class=\"body\">");
            Content.AppendLine("\t<form id=\"form1\" runat=\"server\">");
            Content.AppendLine("\t\t<div id=\"bodyMain\" class=\"bodyMainDiv\">");
            Content.AppendLine("\t\t<!-- Content -->");
            if (!String.IsNullOrEmpty(ContentParts))
            {
                Content.AppendLine(ContentParts);
            }
            Content.AppendLine("\t\t<!-- Content End -->");
            Content.AppendLine("\t\t</div>");
            Content.AppendLine("\t</form>");
            Content.AppendLine("</body>");
            Content.AppendLine("</html>");

            return Content.ToString();
        }

        private DataTable BuildEmptyDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("id", typeof(string)));
            dt.Columns.Add(new DataColumn("Visable", typeof(string)));
            dt.Columns.Add(new DataColumn("Left", typeof(string)));
            dt.Columns.Add(new DataColumn("Top", typeof(string)));
            dt.Columns.Add(new DataColumn("Width", typeof(string)));
            dt.Columns.Add(new DataColumn("Height", typeof(string)));
            dt.Columns.Add(new DataColumn("PrimitiveHeight", typeof(string)));
            dt.Columns.Add(new DataColumn("AscxControlFileName", typeof(string)));
            dt.Columns.Add(new DataColumn("HorizontalAlign", typeof(string)));
            dt.Columns.Add(new DataColumn("VerticalAlign", typeof(string)));
            dt.Columns.Add(new DataColumn("BorderStyle", typeof(string)));
            dt.Columns.Add(new DataColumn("BorderWidth", typeof(string)));
            dt.Columns.Add(new DataColumn("BorderColor", typeof(string)));
            dt.Columns.Add(new DataColumn("BackColor", typeof(string)));
            dt.Columns.Add(new DataColumn("TitleImageUrl", typeof(string)));
            dt.Columns.Add(new DataColumn("BackImageUrl", typeof(string)));
            dt.Columns.Add(new DataColumn("BottomImageUrl", typeof(string)));
            dt.Columns.Add(new DataColumn("AutoHeight", typeof(string)));
            dt.Columns.Add(new DataColumn("ZIndex", typeof(string)));
            dt.Columns.Add(new DataColumn("ParentLeft", typeof(string)));
            dt.Columns.Add(new DataColumn("CssClass", typeof(string)));
            dt.Columns.Add(new DataColumn("TopUpLimit", typeof(string)));
            dt.Columns.Add(new DataColumn("TitleImageUrlLink", typeof(string)));
            dt.Columns.Add(new DataColumn("BottomImageUrlLink", typeof(string)));
            dt.Columns.Add(new DataColumn("TitleImageUrlLinkTarget", typeof(string)));
            dt.Columns.Add(new DataColumn("BottomImageUrlLinkTarget", typeof(string)));
            dt.Columns.Add(new DataColumn("ControlAttributes", typeof(string)));
            dt.Columns.Add(new DataColumn("Lock", typeof(string)));

            return dt;
        }

        private string BuildShoveWebPartContent(DataTable dtParts, ref string RegisterUserControlTagName)
        {
            RegisterUserControlTagName = "";

            if ((dtParts == null) || (dtParts.Rows.Count < 1))
            {
                return "";
            }

            DataRow[] drs = new DataRow[dtParts.Rows.Count];
            dtParts.Rows.CopyTo(drs, 0);

            if (dtParts.Rows.Count > 1)
            {
                Array.Sort(drs, new ShoveWebPartCompareTop());
            }

            StringBuilder Result = new StringBuilder();

            foreach (DataRow dr in drs)
            {
                Result.AppendLine("");
                Result.AppendLine("<!-- Shove.Web.UI.ShoveWebPart Start -->");
                string ParentLeft = dr["ParentLeft"].ToString();
                Result.Append("<div runat=\"server\" ShoveWebUITypeName=\"ShoveWebPart\" OffsetLeft=\"" + ((ParentLeft.ToString() == "") ? "0px" : ParentLeft.ToString()) + "\" ");
                string PrimitiveHeight = dr["PrimitiveHeight"].ToString();
                Result.Append("PrimitiveHeight=\"" + ((PrimitiveHeight.ToString() == "") ? "0px" : PrimitiveHeight.ToString()) + "\" ");
                string TopUpLimit = dr["TopUpLimit"].ToString();
                Result.Append("TopUpLimit=\"" + ((TopUpLimit == "True") ? "True" : "False") + "\" ");
                string id = dr["id"].ToString();
                Result.Append("id=\"" + id + "\" ");
                string Lock = dr["Lock"].ToString();
                Result.Append("Lock=\"" + ((Lock == "True") ? "True" : "False") + "\" ");
                string Visable = dr["Visable"].ToString();
                Result.Append("Visable=\"" + ((Visable == "True") ? "True" : "False") + "\" ");
                Result.Append("style=\"text-align: " + dr["HorizontalAlign"].ToString().ToLower() + "; ");
                Result.Append("vertical-align: " + dr["VerticalAlign"].ToString().ToLower() + "; ");
                string BackColor = dr["BackColor"].ToString();
                if (BackColor.Equals("Window", StringComparison.OrdinalIgnoreCase) || BackColor == "")
                {
                    BackColor = "0";
                }
                Result.Append("background-color: " + BackColor + "; ");
                Result.Append("position: absolute; ");
                Result.Append("border: " + dr["BorderWidth"].ToString() + " " + dr["BorderStyle"].ToString() + " " + dr["BorderColor"].ToString() + "; ");
                Result.Append("top: " + dr["Top"].ToString() + "; left: " + dr["Left"].ToString() + "; width: " + dr["Width"].ToString() + "; ");
                string AutoHeight = dr["AutoHeight"].ToString();
                if (AutoHeight == "True")
                {
                    Result.Append("height: auto; overflow-x: hidden; overflow-y: visible; ");
                }
                else
                {
                    Result.Append("height: " + dr["Height"].ToString() + "; overflow: hidden; ");
                }
                Result.AppendLine("z-index: " + dr["ZIndex"].ToString() + "\">");

                Result.AppendLine("\t<script type=\"text/javascript\">");
                Result.AppendLine("\t\tShoveWebUI_ShoveWebPart_OffsetLeft(\"" + id + "\");");
                Result.AppendLine("\t</script>");

                string BackImageUrl = dr["BackImageUrl"].ToString();
                if (BackImageUrl.Trim() != "")
                {
                    BackImageUrl = "~/" + BackImageUrl;
                }
                Result.AppendLine(String.Format("\t<table id=\"{0}_TableContent\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style='width: 100%; height: 100%; background-image:url(<%= ResolveUrl(\"{1}\") %>)'>", id, BackImageUrl));
                Result.AppendLine("\t\t<tr>");
                string TitleImageUrl = dr["TitleImageUrl"].ToString();
                string TitleImageUrlLink = dr["TitleImageUrlLink"].ToString();
                if (TitleImageUrl.Trim() != "")
                {
                    TitleImageUrl = "~/" + TitleImageUrl;

                    if (TitleImageUrlLink != "")
                    {
                        Result.AppendLine(String.Format("\t\t\t<td valign=\"top\"><a id=\"{0}_TitleImageLink\" href=\"{1}\" target=\"{2}\"><img id=\"{3}_TitleImage\" src='<%= ResolveUrl(\"{4}\") %>' alt=\"\" style=\"border: 0px; display: block;\" /></a></td>", id, TitleImageUrlLink, dr["TitleImageUrlLinkTarget"].ToString(), id, TitleImageUrl));
                    }
                    else
                    {
                        Result.AppendLine(String.Format("\t\t\t<td valign=\"top\"><img id=\"{0}_TitleImage\" src='<%= ResolveUrl(\"{1}\") %>' alt=\"\" style=\"border: 0px; display: block;\" /></td>", id, TitleImageUrl));
                    }
                }
                else
                {
                    Result.AppendLine(String.Format("\t\t\t<td valign=\"top\"><img id=\"{0}_TitleImage\" src=\"about:blank\" alt=\"\" style=\"display: none;\" /></td>", id));
                }
                Result.AppendLine("\t\t</tr>");

                Result.AppendLine(String.Format("\t\t<tr id=\"{0}_Content\">", id));
                Result.AppendLine(String.Format("\t\t\t<td id=\"{0}_ContentTD\" class=\"{1}\" align=\"{2}\" valign=\"{3}\">", id, dr["CssClass"].ToString(), dr["HorizontalAlign"].ToString().ToLower(), dr["VerticalAlign"].ToString().ToLower()));
                string AscxControlFileName = dr["AscxControlFileName"].ToString();
                string ControlAttributes = dr["ControlAttributes"].ToString();
                if (AscxControlFileName == "")
                {
                    Result.AppendLine("\t\t\t\t<!-- No Content -->");
                }
                else
                {
                    string UserControlTagName = GetUserControlTagName(AscxControlFileName);
                    string UserControlID = GetNewUserControlID(Result.ToString(), UserControlTagName);
                    Result.AppendLine(String.Format("\t\t\t\t<ShoveWebUI:{0} ID=\"{1}\" runat=\"server\" ControlAttributes=\"{2}\" SiteID=\"{3}\" />", UserControlTagName, UserControlID, ControlAttributes, SiteID));
                }
                Result.AppendLine("\t\t\t</td>");
                Result.AppendLine("\t\t</tr>");

                Result.AppendLine("\t\t<tr>");
                string BottomImageUrl = dr["BottomImageUrl"].ToString();
                string BottomImageUrlLink = dr["BottomImageUrlLink"].ToString();
                if (BottomImageUrl.Trim() != "")
                {
                    BottomImageUrl = "~/" + BottomImageUrl;

                    if (BottomImageUrlLink != "")
                    {
                        Result.AppendLine(String.Format("\t\t\t<td valign=\"bottom\"><a id=\"{0}_BottomImageLink\" href=\"{1}\" target=\"{2}\"><img id=\"{3}_BottomImage\" src='<%= ResolveUrl(\"{4}\") %>' alt=\"\" style=\"border: 0px; display: block;\" /></a></td>", id, BottomImageUrlLink, dr["BottomImageUrlLinkTarget"].ToString(), id, BottomImageUrl));
                    }
                    else
                    {
                        Result.AppendLine(String.Format("\t\t\t<td valign=\"bottom\"><img id=\"{0}_BottomImage\" src='<%= ResolveUrl(\"{1}\") %>' alt=\"\" style=\"border: 0px; display: block;\" /></td>", id, BottomImageUrl));
                    }
                }
                else
                {
                    Result.AppendLine(String.Format("\t\t\t<td valign=\"bottom\"><img id=\"{0}_BottomImage\" src=\"about:blank\" alt=\"\" style=\"display: none;\" /></td>", id));
                }
                Result.AppendLine("\t\t</tr>");

                Result.AppendLine("\t</table>");
                Result.AppendLine("</div>");
                Result.AppendLine("<!-- Shove.Web.UI.ShoveWebPart End -->");
            }

            RegisterUserControlTagName = BuildRegisterUserControlTagName(drs);

            return Result.ToString();
        }

        #region 输出用户控件相关

        /// <summary>
        /// 根据用户控件的文件名，获取一个 TagName
        /// </summary>
        /// <param name="AscxFileName"></param>
        /// <returns></returns>
        private string GetUserControlTagName(string AscxFileName)
        {
            if (String.IsNullOrEmpty(AscxFileName))
            {
                return "";
            }

            if (AscxFileName.StartsWith("~/"))
            {
                AscxFileName = AscxFileName.Substring(2);
            }

            if (AscxFileName.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
            {
                AscxFileName = AscxFileName.Substring(0, AscxFileName.Length - 5);
            }

            return AscxFileName.Replace("/", "_");
        }

        /// <summary>
        /// 根据所有的控件，生成控件引用的 TagName 列表
        /// </summary>
        /// <param name="drs"></param>
        /// <returns></returns>
        private string BuildRegisterUserControlTagName(DataRow[] drs)
        {
            if ((drs == null) || (drs.Length < 1))
            {
                return "";
            }

            string Result = "\r\n";

            foreach (DataRow dr in drs)
            {
                if (Result.Contains("src=\"" + dr["AscxControlFileName"].ToString() + "\""))
                {
                    continue;
                }

                string AscxControlFileName = dr["AscxControlFileName"].ToString();
                string UserControlTagName = GetUserControlTagName(AscxControlFileName);
                
                if (!String.IsNullOrEmpty(UserControlTagName))
                {
                    Result += "<%@ Register src=\"" + AscxControlFileName + "\" tagname=\"" + GetUserControlTagName(AscxControlFileName) + "\" tagprefix=\"ShoveWebUI\" %>\r\n";
                }
            }

            return Result;
        }

        private DataTable GetUserControlTagNames(string FileContent)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("TagName", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));

            Regex regex = new Regex(@"<%@ Register src=""(?<L0>[^""]*)"" tagname=""(?<L1>[^""]*)"" tagprefix=""ShoveWebUI"" %>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            for (Match m = regex.Match(FileContent); m.Success; m = m.NextMatch())
            {
                DataRow dr = dt.NewRow();

                dr["FileName"] = m.Groups["L0"].Value;
                dr["TagName"] = m.Groups["L1"].Value;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 从一个用户控件的 TagName, 得到它的实际文件名
        /// </summary>
        /// <param name="dtUserControlTagNames"></param>
        /// <param name="TagName"></param>
        /// <returns></returns>
        private string GetUserControlFileNameFromTagName(DataTable dtUserControlTagNames, string TagName)
        {
            if ((dtUserControlTagNames == null) || (dtUserControlTagNames.Rows.Count < 1))
            {
                return "";
            }

            int RowNo = PublicFunction.FindRow(dtUserControlTagNames, "TagName", TagName);

            if (RowNo < 0)
            {
                return "";
            }

            return dtUserControlTagNames.Rows[RowNo]["FileName"].ToString();
        }

        /// <summary>
        /// 获取一个新的用户控件的 ID
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="UserControlTagName"></param>
        /// <returns></returns>
        private string GetNewUserControlID(string Html, string UserControlTagName)
        {
            int i = 1;

            while (true)
            {
                string ID = UserControlTagName + i.ToString();
                string Content = "<ShoveWebUI:" + UserControlTagName + " ID=\"" + UserControlTagName + i.ToString();

                if (Html.Contains(Content))
                {
                    i++;

                    continue;
                }

                return UserControlTagName + i.ToString();
            }
        }

        #endregion

        private DataTable LayerInCache
        {
            get
            {
                if (String.IsNullOrEmpty(PageName))
                {
                    return null;
                }

                object obj = System.Web.HttpContext.Current.Cache["ShoveWebPart.4.Layer_" + SiteID.ToString() + "_" + PageName];

                if (obj == null)
                {
                    return null;
                }

                DataTable Result = null;

                try
                {
                    Result = (DataTable)obj;
                }
                catch { }

                return Result;
            }

            set
            {
                if (String.IsNullOrEmpty(PageName))
                {
                    return;
                }

                string Key = "ShoveWebPart.4.Layer_" + SiteID.ToString() + "_" + PageName;
                System.Web.HttpContext.Current.Cache.Remove(Key);

                if (value != null)
                {
                    System.Web.HttpContext.Current.Cache.Insert(Key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60));
                }
            }
        }

        /// <summary>
        /// 读取 Part 到 DataTable
        /// </summary>
        /// <param name="OtherFileName">不指定另外的文件名，则用本类的文件名 FileName</param>
        /// <param name="ShoveWebPartID">如果指定了 PartID, 则将后面的 RowNo 参数赋值，标识这个 Part 的行号</param>
        /// <param name="RowNo">指定的 Part 的行号</param>
        /// <returns></returns>
        public DataTable Read(string OtherFileName, string ShoveWebPartID, ref int RowNo)
        {
            RowNo = -1;
            DataTable dtParts = null;

            if (String.IsNullOrEmpty(OtherFileName))
            {
                dtParts = LayerInCache;
            }

            if (dtParts == null)
            {
                dtParts = BuildEmptyDataTable();
                string FileContent = "";

                try
                {
                    if (String.IsNullOrEmpty(OtherFileName))
                    {
                        FileContent = System.IO.File.ReadAllText(FileName);
                    }
                    else
                    {
                        FileContent = System.IO.File.ReadAllText(OtherFileName);
                    }
                }
                catch
                {
                    return dtParts;
                }

                DataTable dtUserControlTagNames = GetUserControlTagNames(FileContent);
                Regex regex = new Regex(@"<div runat=""server"" ShoveWebUITypeName=""ShoveWebPart"" OffsetLeft=""(?<L0>[^""]*)"" PrimitiveHeight=""(?<L1>[^""]*)"" TopUpLimit=""(?<L2>[^""]*)"" id=""(?<L3>[^""]+)"" Lock=""(?<L4>[^""]*)"" Visable=""(?<L5>[^""]*)"" style=""text-align: (?<L6>[^;]*); vertical-align: (?<L7>[^;]*); background-color: (?<L8>[^;]*); position: absolute; border: (?<L9>[^\s]*?) (?<L10>[^\s]*?) (?<L11>[^;]*); top: (?<L12>[^;]*); left: (?<L13>[^;]*); width: (?<L14>[^;]*); height: (?<L15>[^;]*); ((overflow: hidden;)|(overflow-x: hidden; overflow-y: visible;)) z-index: (?<L16>[^""]*)"">[\S\s]+?background-image:url[(]<[%][=] ResolveUrl[(]""(?<L17>[^""]*)""[)] [%]>[)][\S\s]+?<td valign=""top"">(<a id=""[^""]+?"" href=""(?<L18>[^""]*)"" target=""(?<L19>[^""]*)"">){0,1}<img id=""[^""]+?"" src=['""]{1}(?<L20>[\S\s]+?)['""]{1}[^>]+?>(</a>){0,1}</td>[\S\s]+?<td id=""[^""]+?"" class=""(?<L21>[^""]*)""[^<]+?<[\S\s]*?(ShoveWebUI:(?<L22>[^ ]+?) ID=""[^""]+"" runat=""server"" ControlAttributes=""(?<L23>[^""]*)"" SiteID=""[^""]+"" />){0,1}[\S\s]+?<td valign=""bottom"">(<a id=""[^""]+?"" href=""(?<L24>[^""]*)"" target=""(?<L25>[^""]*)"">){0,1}<img id=""[^""]+?"" src=['""]{1}(?<L26>[\S\s]+?)['""]{1}[^>]+?>(</a>){0,1}</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                for (Match m = regex.Match(FileContent); m.Success; m = m.NextMatch())
                {
                    DataRow dr = dtParts.NewRow();

                    #region Div

                    dr["ParentLeft"] = m.Groups["L0"].Value;
                    dr["PrimitiveHeight"] = m.Groups["L1"].Value;
                    dr["TopUpLimit"] = m.Groups["L2"].Value;
                    dr["id"] = m.Groups["L3"].Value;
                    dr["Lock"] = m.Groups["L4"].Value;
                    dr["Visable"] = m.Groups["L5"].Value;
                    dr["HorizontalAlign"] = m.Groups["L6"].Value;
                    dr["VerticalAlign"] = m.Groups["L7"].Value;
                    string BackColor = m.Groups["L8"].Value;
                    if (BackColor.Equals("Window", StringComparison.OrdinalIgnoreCase) || BackColor == "0")
                    {
                        BackColor = "";
                    }
                    dr["BackColor"] = BackColor;
                    dr["BorderWidth"] = m.Groups["L9"].Value;
                    dr["BorderStyle"] = m.Groups["L10"].Value;
                    dr["BorderColor"] = m.Groups["L11"].Value;
                    dr["Top"] = m.Groups["L12"].Value;
                    dr["Left"] = m.Groups["L13"].Value;
                    dr["Width"] = m.Groups["L14"].Value;
                    dr["Height"] = m.Groups["L15"].Value;
                    dr["AutoHeight"] = (m.Groups["L15"].Value == "auto").ToString();
                    dr["ZIndex"] = m.Groups["L16"].Value;

                    string BackImageUrl = m.Groups["L17"].Value;
                    if (BackImageUrl.StartsWith("~/"))
                    {
                        BackImageUrl = BackImageUrl.Substring(2);
                    }
                    dr["BackImageUrl"] = BackImageUrl;

                    #endregion

                    #region TitleImage

                    string TitleImageUrl = m.Groups["L20"].Value;

                    if ((TitleImageUrl == "") || (TitleImageUrl == "about:blank"))
                    {
                        dr["TitleImageUrl"] = "";
                        dr["TitleImageUrlLink"] = "";
                        dr["TitleImageUrlLinkTarget"] = "_self";
                    }
                    else
                    {
                        if (TitleImageUrl.StartsWith("<%= ResolveUrl(\"~/"))
                        {
                            TitleImageUrl = TitleImageUrl.Substring(18, TitleImageUrl.Length - 23);
                        }

                        dr["TitleImageUrl"] = TitleImageUrl;
                        dr["TitleImageUrlLink"] = m.Groups["L18"].Value;
                        dr["TitleImageUrlLinkTarget"] = m.Groups["L19"].Value;
                    }

                    #endregion

                    dr["CssClass"] = m.Groups["L21"].Value;

                    #region Control

                    string AscxControlFileName = m.Groups["L22"].Value;

                    if (AscxControlFileName == "")
                    {
                        dr["ControlAttributes"] = "";
                    }
                    else
                    {
                        AscxControlFileName = GetUserControlFileNameFromTagName(dtUserControlTagNames, AscxControlFileName);

                        dr["AscxControlFileName"] = AscxControlFileName;
                        dr["ControlAttributes"] = m.Groups["L23"].Value;
                    }

                    #endregion

                    #region BottomImage

                    string BottomImageUrl = m.Groups["L26"].Value;

                    if ((BottomImageUrl == "") || (BottomImageUrl == "about:blank"))
                    {
                        dr["BottomImageUrl"] = "";
                        dr["BottomImageUrlLink"] = "";
                        dr["BottomImageUrlLinkTarget"] = "_self";
                    }
                    else
                    {
                        if (BottomImageUrl.StartsWith("<%= ResolveUrl(\"~/"))
                        {
                            BottomImageUrl = BottomImageUrl.Substring(18, BottomImageUrl.Length - 23);
                        }

                        dr["BottomImageUrl"] = BottomImageUrl;
                        dr["BottomImageUrlLink"] = m.Groups["L24"].Value;
                        dr["BottomImageUrlLinkTarget"] = m.Groups["L25"].Value;
                    }

                    #endregion

                    dtParts.Rows.Add(dr);
                }

                if (String.IsNullOrEmpty(OtherFileName))
                {
                    LayerInCache = dtParts;
                }
            }

            if (dtParts.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(ShoveWebPartID))
                {
                    RowNo = PublicFunction.FindRow(dtParts, "id", ShoveWebPartID);
                }
            }

            return dtParts;
        }

        /// <summary>
        /// 根据 drParts 的 Part 写文件
        /// </summary>
        /// <param name="dtParts">Part 内容表格</param>
        /// <param name="OtherFileName">如果指定了其他文件名，则写其他文件名，否则写入本类 FileName</param>
        public void Write(DataTable dtParts, string OtherFileName)
        {
            if (String.IsNullOrEmpty(FileName) && String.IsNullOrEmpty(OtherFileName))
            {
                return;
            }

            string RegisterUserControlTagName = "";
            string ShoveWebPartContent = BuildShoveWebPartContent(dtParts, ref RegisterUserControlTagName);
            string FileContent = BuildFile(ShoveWebPartContent, RegisterUserControlTagName);

            System.IO.File.WriteAllText(String.IsNullOrEmpty(FileName) ? OtherFileName : FileName, FileContent);
        }

        /// <summary>
        /// 删除一个 ShoveWebPart, 
        /// </summary>
        /// <param name="id">ShoveWebPart 的 ID</param>
        public void Delete(string id)
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, id, ref RowNo);

            if ((dtParts == null) || (RowNo < 0))
            {
                return;
            }

            dtParts.Rows.Remove(dtParts.Rows[RowNo]);

            Write(dtParts, null);
        }

        /// <summary>
        /// 保存修改多个 ShoveWebPart 的 zIndex 属性
        /// </summary>
        /// <param name="ZIndexList">各个需要修改 zIndex 属性的控件、zIndex 列表，格式： id1:值;id2:值;... </param>
        public void SaveZIndex(string[] ZIndexList)
        {
            if ((ZIndexList == null) || (ZIndexList.Length < 1))
            {
                return;
            }

            // 逐个修改 zIndex

            int RowNo = -1;
            DataTable dtParts = Read(null, null, ref RowNo);

            if ((dtParts == null) || (dtParts.Rows.Count < 1))
            {
                return;
            }

            bool isEdited = false;

            foreach (string str in ZIndexList)
            {
                string[] objIndex = str.Split(':');

                if ((objIndex == null) || (objIndex.Length != 2))
                {
                    continue;
                }

                string t_id = objIndex[0];
                int t_index = 0;
                try { t_index = int.Parse(objIndex[1]); }
                catch { }

                int RowIndex = PublicFunction.FindRow(dtParts, "id", t_id);

                if (RowIndex < 0)
                {
                    continue;
                    //return "ShoveWebPart 部件不存在，可能已经被删除。";
                }

                dtParts.Rows[RowIndex]["ZIndex"] = t_index.ToString();
                isEdited = true;
            }

            if (isEdited)
            {
                dtParts.AcceptChanges();
            }

            Write(dtParts, null);
        }

        /// <summary>
        /// 保存修改某个 ShoveWebPart 的 LockState
        /// </summary>
        /// <param name="id"></param>
        /// <param name="LockState"></param>
        public void SaveLock(string id, bool LockState)
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, id, ref RowNo);

            if ((dtParts == null) || (RowNo < 0))
            {
                return;
            }

            dtParts.Rows[RowNo]["Lock"] = LockState.ToString();
            dtParts.AcceptChanges();

            Write(dtParts, null);
        }

        /// <summary>
        /// 将内容属性都是 OldAttributes 的 ShoveWebPart 的所有属性应用到所有的其他页的相同内容的 ShoveWebPart 控件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ApplyToAllPage"></param>
        /// <param name="AddToNoExistPage">一个不存在这个内容的 ShoveWebPart 的页中，是否也要增加一个控件</param>
        /// <param name="OldAttributes"></param>
        public void SaveApplyToAll(string id, bool ApplyToAllPage, bool AddToNoExistPage, string OldAttributes)
        {
            if (!ApplyToAllPage && !AddToNoExistPage)
            {
                return;
            }

            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches");
            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx");
            if (files.Length == 0)
            {
                return;
            }

            int RowNo = -1;
            DataTable dtParts = Read(null, id, ref RowNo);

            if ((dtParts == null) || (RowNo < 0))
            {
                return;
            }

            if (dtParts.Rows[RowNo]["AscxControlFileName"].ToString() == "")
            {
                return;
            }

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".aspx")
                {
                    continue;
                }

                string t_FileName = file.Name.Substring(0, file.Name.Length - 5);
                int t_RowNo = -1;
                DataTable t_dtParts = Read(t_FileName, null, ref t_RowNo);

                if (!ApplyToAllPage || (dtParts.Rows.Count < 1))
                {
                    goto label_Add;
                }

                DataRow[] drs = t_dtParts.Select("AscxControlFileName='" + dtParts.Rows[RowNo]["AscxControlFileName"].ToString() + "' and ControlAttributes='" + OldAttributes + "'");

                if ((drs == null) || (drs.Length < 1))
                {
                    goto label_Add;
                }

                bool isEdited = false;

                foreach (DataRow dr in drs)
                {
                    string t_id = dr["id"].ToString();

                    if ((t_FileName == FileName) && (t_id == dtParts.Rows[RowNo]["id"].ToString()))
                    {
                        continue;
                    }

                    isEdited = true;

                    dr.ItemArray = dtParts.Rows[RowNo].ItemArray;
                    dr["id"] = t_id;
                }

                if (isEdited)
                {
                    t_dtParts.AcceptChanges();
                    Write(t_dtParts, t_FileName);

                    continue;
                }

            label_Add:

                if ((!AddToNoExistPage) || (t_FileName == FileName))
                {
                    continue;
                }

                DataRow drNew = t_dtParts.NewRow();
                drNew.ItemArray = dtParts.Rows[RowNo].ItemArray;
                drNew["id"] = GetNewPartID(t_dtParts);
                t_dtParts.Rows.Add(drNew);

                Write(t_dtParts, t_FileName);
            }
        }

        /// <summary>
        /// 将控件置为底层
        /// </summary>
        /// <param name="id"></param>
        /// <param name="zIndex"></param>
        public void ToBackground(string id, int zIndex)
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, id, ref RowNo);

            if ((dtParts == null) || (RowNo < 0))
            {
                return;
            }

            dtParts.Rows[RowNo]["zIndex"] = zIndex;
            dtParts.AcceptChanges();

            Write(dtParts, null);
        }

        /// <summary>
        /// 保存键盘移动后的位置
        /// </summary>
        /// <param name="Datas"></param>
        public void SaveForKeyMove(string[] Datas)
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, null, ref RowNo);

            if ((dtParts == null) || (dtParts.Rows.Count < 1))
            {
                return;
            }

            bool isEdited = false;

            foreach (string Data in Datas)
            {
                if (String.IsNullOrEmpty(Data))
                {
                    continue;
                }

                string[] strs = Data.Split(',');

                if (strs.Length != 3)
                {
                    continue;
                }

                string t_id = strs[0];
                string t_left = strs[1];
                string t_top = strs[2];

                int RowIndex = PublicFunction.FindRow(dtParts, "id", t_id);

                if (RowIndex < 0)
                {
                    continue;
                }

                dtParts.Rows[RowIndex]["Left"] = t_left;
                dtParts.Rows[RowIndex]["Top"] = t_top;
                isEdited = true;
            }

            if (isEdited)
            {
                dtParts.AcceptChanges();
            }

            Write(dtParts, null);
        }

        /// <summary>
        /// 通过键盘删除Part后保存数据
        /// </summary>
        /// <param name="Datas"></param>
        public void DeleteForKeyMove(string[] Datas)
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, null, ref RowNo);

            if ((dtParts == null) || (dtParts.Rows.Count < 1))
            {
                return;
            }

            bool isEdited = false;

            foreach (string t_id in Datas)
            {
                if (String.IsNullOrEmpty(t_id))
                {
                    continue;
                }

                int RowIndex = PublicFunction.FindRow(dtParts, "id", t_id);

                if (RowIndex < 0)
                {
                    continue;
                }

                dtParts.Rows.Remove(dtParts.Rows[RowIndex]);
                isEdited = true;
            }

            if (isEdited)
            {
                Write(dtParts, null);
            }
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="SourceParts"></param>
        public void Paste(string[] SourceParts)
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, null, ref RowNo);

            if (dtParts == null)
            {
                return;
            }

            int Count = 0;

            for (int i = 0; i < SourceParts.Length; i++)
            {
                int t_RowNo = -1;
                DataTable t_dtParts = Read(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches/" + SourceParts[i].Split('.')[0] + ".aspx", SourceParts[i].Split('.')[1], ref t_RowNo);

                if ((t_dtParts == null) || (t_RowNo < 0))
                {
                    continue;
                }

                Count++;

                string NewID = GetNewPartID(dtParts);

                DataRow dr = dtParts.NewRow();
                dr.ItemArray = t_dtParts.Rows[t_RowNo].ItemArray;
                dr["id"] = NewID;
                dtParts.Rows.Add(dr);
            }

            if (Count > 0)
            {
                Write(dtParts, null);
            }
        }

        /// <summary>
        /// 将所有的 Part 的 ZIndex 减去 1
        /// </summary>
        public void ZIndexSubtractOne()
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, null, ref RowNo);

            if ((dtParts == null) || (dtParts.Rows.Count < 1))
            {
                return;
            }

            for (int i = 0; i < dtParts.Rows.Count; i++)
            {
                int ZIndex = 5000;

                try
                {
                    ZIndex = int.Parse(dtParts.Rows[i]["ZIndex"].ToString());
                }
                catch { }

                dtParts.Rows[i]["ZIndex"] = ZIndex - 1;
            }

            dtParts.AcceptChanges();

            Write(dtParts, null);
        }

        /// <summary>
        /// 获取一个新的 ShoveWebPart ID
        /// </summary>
        /// <returns></returns>
        public string GetNewPartID(DataTable dtParts)
        {
            int i = 1;

            while (dtParts.Select("id='ShoveWebPart" + i.ToString() + "'").Length > 0)
            {
                i++;
            }

            return "ShoveWebPart" + i.ToString();
        }

        /// <summary>
        /// 获取一个新的 ShoveWebPart ID
        /// </summary>
        /// <returns></returns>
        public string GetNewPartID()
        {
            int RowNo = -1;
            DataTable dtParts = Read(null, null, ref RowNo);

            return GetNewPartID(dtParts);
        }

        /// <summary>
        /// 获取时间串构成的备份文件名
        /// </summary>
        /// <returns></returns>
        private string GetNewBackupFileName()
        {
            return String.Format("{0}-{1}-{2}_{3}-{4}-{5}", System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
        }

        /// <summary>
        /// 备份整个站点页面，备份在 Backup/Site/时间串/*.aspx.bak
        /// </summary>
        public void BackupSite()
        {
            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches");

            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx");
            if (files.Length == 0)
            {
                return;
            }

            string Path = System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches/Backup/Site/" + GetNewBackupFileName();

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".aspx")
                {
                    continue;
                }

                string t_FileName = Path + "/" + file.Name + ".bak";

                System.IO.File.Copy(file.FullName, t_FileName, true);
            }
        }

        /// <summary>
        /// 恢复整个站点页面，先删除站点页面，再用 Backup/Site/时间串/*.aspx.bak 的文件还原
        /// </summary>
        /// <param name="BackupDirectoryName"></param>
        public void RestoreSite(string BackupDirectoryName)
        {
            if (String.IsNullOrEmpty(BackupDirectoryName))
            {
                DeleteSite();

                return;
            }

            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches");

            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx");
            
            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".aspx")
                {
                    continue;
                }

                System.IO.File.Delete(file.FullName);
            }

            di = new DirectoryInfo(BackupDirectoryName);

            if (!di.Exists)
            {
                return;
            }

            files = di.GetFiles("*.aspx.bak");

            if (files.Length == 0)
            {
                return;
            }

            string Path = System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches";

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".bak")
                {
                    continue;
                }

                string t_FileName = Path + "/" + file.Name.Substring(0, file.Name.LastIndexOf("."));

                System.IO.File.Copy(file.FullName, t_FileName, true);
            }

            ClearSiteCache();
        }

        /// <summary>
        /// 删除整个站点页面
        /// </summary>
        public void DeleteSite()
        {
            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches");

            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx");

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() != ".aspx")
                {
                    continue;
                }

                System.IO.File.Delete(file.FullName);
            }

            ClearSiteCache();
        }

        private void ClearSiteCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();

            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }

            foreach (string key in al)
            {
                if (key.StartsWith("ShoveWebPart.4.Layer_" + SiteID.ToString() + "_", StringComparison.OrdinalIgnoreCase))
                {
                    cache.Remove(key);
                }
            }
        }

        /// <summary>
        /// 删除站点某个备份
        /// </summary>
        /// <param name="BackupDirectoryName"></param>
        public void DeleteSiteBackup(string BackupDirectoryName)
        {
            if (!System.IO.Directory.Exists(BackupDirectoryName))
            {
                return;
            }

            System.IO.Directory.Delete(BackupDirectoryName);
        }

        /// <summary>
        /// 备份页面，备份在 Backup/Pages/页名/时间串.aspx.bak
        /// </summary>
        public void BackupPage()
        {
            string Path = System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/PageCaches/Backup/Pages/" + PageName;

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            string TargetFileName = Path + "/" + GetNewBackupFileName() + ".aspx.bak";

            if (!System.IO.File.Exists(FileName))
            {
                return;
            }

            System.IO.File.Copy(FileName, TargetFileName, true);
        }

        /// <summary>
        /// 恢复页面
        /// </summary>
        /// <param name="BackupFileName"></param>
        public void RestorePage(string BackupFileName)
        {
            if (String.IsNullOrEmpty(BackupFileName))
            {
                System.IO.File.WriteAllText(FileName, BuildFile(null, null));

                return;
            }

            if (!System.IO.File.Exists(BackupFileName))
            {
                return;
            }

            System.IO.File.Copy(BackupFileName, FileName, true);

            LayerInCache = null;
        }

        /// <summary>
        /// 删除页面
        /// </summary>
        public void DeletePage()
        {
            if (!System.IO.File.Exists(FileName))
            {
                return;
            }

            System.IO.File.Delete(FileName);

            LayerInCache = null;
        }

        /// <summary>
        /// 删除页面备份文件
        /// </summary>
        /// <param name="BackupFileName"></param>
        public void DeletePageBackup(string BackupFileName)
        {
            if (!System.IO.File.Exists(BackupFileName))
            {
                return;
            }

            System.IO.File.Delete(BackupFileName);
        }

        /// <summary>
        /// 从其他页面复制内容
        /// </summary>
        /// <param name="SourceFileName"></param>
        public void CopyPage(string SourceFileName)
        {
            if (!System.IO.File.Exists(SourceFileName))
            {
                return;
            }

            System.IO.File.Copy(SourceFileName, FileName, true);

            LayerInCache = null;
        }
    }
}
