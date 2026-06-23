using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Utilities
{
    public static class EmailTemplate
    {

        public static string WelcomeMember(
            string firstName,
            string ChurchName,
            string Email,
            string Password
        )
        {
            var body = $@"
                <p>Hello {firstName},</p>

                <p>
                    Welcome to <strong>{ChurchName}</strong>.
                </p>

                <p>
                    Your member account has been created successfully.
                </p>
                 <p>
                    Kindly use the the below credentials to login to your employee portal at www.faithconnect.ng and don't forget to change your password.
                </p>
                 <p>
                    {Email}
                    {Password}
                </p>
                <p>
                    We are excited to have you onboard.
                </p>
            ";

            return EmailLayout.Build(
                "Welcome to FaithConnect",
                body
            );
        }
    }
}
