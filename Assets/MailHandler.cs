using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class MailHandler : MonoBehaviour {
	public static MailHandler mee;
	void Awake(){
		mee = this;
	}
	IEnumerator SendMyMail(string bodymsg){
		yield return new WaitForSeconds (1);

		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("reviewmaster201@gmail.com");
		mail.To.Add("reviewmaster201@gmail.com");
		mail.Subject = Application.productName+" Review : ";
		//9mail.Body =PlayFabManager.PlayFabUserID+"\n"+ bodymsg;

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("reviewmaster201@gmail.com", "cgunkomlp") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");
	}
	public void SendMail (string bodymsg)
	{
		StartCoroutine (SendMyMail (bodymsg));

	}
}
