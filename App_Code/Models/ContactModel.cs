using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContactModel
/// </summary>
public class ContactModel
{
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string IndustryList { get; set; }
    public string CountryList { get; set; }
    public string intrestedIn { get; set; }
    public string NodeId { get; set; }
    
}
public class BusinessProblemModel
{
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string NodeId { get; set; }

}
public class HomeContactModel
{
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string NodeId { get; set; }
}
public class ApplyNowModel
{
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string NodeId { get; set; }
}
public class BlogCommentModel
{
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public string BlogPostID { get; set; }
    public string NodeID { get; set; }
}

public class NewletterModel
{
    public string Email { get; set; }
    public string NodeId { get; set; }
    public string Currenturl { get; set; }
}