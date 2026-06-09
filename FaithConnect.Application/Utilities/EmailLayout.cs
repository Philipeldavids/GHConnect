using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Utilities
{
    public static class EmailLayout
    {
        private const string LogoUrl = "https://res.cloudinary.com/dck7rspdt/image/upload/v1781008293/GhConnectLogo_n8yk7q.png";
        public static string Build(
            string title,
            string body
        )
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='UTF-8' />
            </head>

            <body style='margin:0;padding:0;background:#f4f7fb;font-family:Arial,sans-serif;'>

                <table width='100%' cellpadding='0' cellspacing='0'>
                    <tr>
                        <td align='center' style='padding:40px 20px;'>

                            <table width='600' cellpadding='0' cellspacing='0'
                                   style='background:#ffffff;border-radius:12px;overflow:hidden;'>

                                <tr>
    <td style='background:#2563eb;
               padding:24px;
               text-align:center;'>

        <img
            src='{LogoUrl}'
            alt='GHConnect'
            style='max-height:70px;
                   width:auto;
                   display:block;
                   margin:0 auto;' />

    </td>
</tr>

                                <tr>
                                    <td style='padding:32px;'>

                                        <h2 style='margin-top:0;color:#111827;'>
                                            {title}
                                        </h2>

                                        {body}

                                    </td>
                                </tr>

                                <tr>
                                    <td style='padding:20px;text-align:center;
                                               background:#f9fafb;
                                               color:#6b7280;
                                               font-size:12px;'>

                                        © {DateTime.UtcNow.Year} GHConnect.
                                        All rights reserved.

                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>
                </table>

            </body>
            </html>
            ";
        }
    }
}
