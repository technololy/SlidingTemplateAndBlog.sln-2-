﻿using ButterCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BlogModel;

public partial class _Default : Page
{
    public string html { get; set; }
    List<item> blogItems = new List<item>();
    string theWholeHtml { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        var butter = new ButterCMSService();
        if (butter.blogPost != null && butter.blogPost.Data.Count() > 0)
        {
            SetUpBlogDisplay(butter.blogPost);
            return;
        }



    }

    private void SetUpBlogDisplay(PostsResponse blogPost)
    {
        foreach (var item in blogPost.Data)
        {
            theWholeHtml = readFromTextFile();
            theWholeHtml = theWholeHtml.Replace("%image%", item.FeaturedImage);
            theWholeHtml = theWholeHtml.Replace("%subject1%", item.Title);
            theWholeHtml = theWholeHtml.Replace("%subject2%", item.SeoTitle);
            theWholeHtml = theWholeHtml.Replace("%summary%", item.Summary);
            theWholeHtml = theWholeHtml.Replace("%link%", item.Url);

            literalBlog.Text += theWholeHtml;

        }
        literalBlog.Visible = true;
    }

    private string readFromTextFile()
    {
        string path = Server.MapPath("~/TemplateForBlog.txt");
        string read = File.ReadAllText(path);

        return read;
    }
}