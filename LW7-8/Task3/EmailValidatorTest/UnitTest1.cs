using Task3.Services;

namespace EmailValidatorTest
{
    public class UnitTest1
    {

        public static IEnumerable<object[]> ValidEmails()
        {
            return new List<object[]>
            {
                new object[] { "testEmail@example.com" },
                new object[] { "user.Tim+2025@sub.domain.com" },
                new object[] { "12345@numeric-gmail.com" }
            };
        }
        public static IEnumerable<object[]> InvalidEmails()
        {
            return new List<object[]>
            {
                new object[] { "email" },
                new object[] { "email@gmail" },
                new object[] { "emailgmail.com" },
                new object[] { "emailGmail." },
                new object[] { "email@.com" },
                new object[] { "email.com" },
            };
        }
        public static IEnumerable<object[]> Domains()
        {
            return new List<object[]>
            {
                new object[] { "email@gmail.com", "gmail.com" },
                new object[] { "email@Gmail.com", "gmail.com" },
                new object[] { "email@Gmail.COM", "gmail.com" },
                new object[] { "email@some-domain.net", "some-domain.net"},
                new object[] { "email@some-domain.ua.com", "some-domain.ua.com"}
            };
        }
        public static IEnumerable<object[]> InvalidDomains()
        {
            return new List<object[]>
            {
                new object[] { ""},
                new object[] { " " },
                new object[] { null }, 
            };
        }
        public static IEnumerable<object[]> SetNormalGmailEmails()
        {
            return new List<object[]>
            {
                new object[] { "testEmail@example.com","testemail@gmail.com"  },
                new object[] { "user.Tim+2025@sub.domain.com", "usertim@gmail.com" },
                new object[] { "12345@numeric-gmail.com" , "12345@gmail.com" },
                new object[] { "testEmail@gmail.com", "testemail@gmail.com"  }
            };
        }

        [Theory]
        [MemberData(nameof(ValidEmails))]
        public void ValidEmail(string email)
        {
            var valid = EmailValidator.IsValidEmail(email);
            Assert.True(valid);
        }

        [Theory]
        [MemberData(nameof(InvalidEmails))]
        public void InvalidEmail(string email)
        {
            var valid = EmailValidator.IsValidEmail(email);
            Assert.False(valid);
        }


        [Theory]
        [MemberData(nameof(Domains))]
        public void Domain(string email, string domain)
        {
            string dom = EmailValidator.GetDomain(email);

            Assert.Equal(domain, dom);
        }

        [Theory]
        [MemberData(nameof(InvalidDomains))]
        [MemberData(nameof(InvalidEmails))]
        public void InvalidDomain(string email)
        {
            Assert.Throws<FormatException>(() => EmailValidator.GetDomain(email));
        }


        [Theory]
        [MemberData(nameof(SetNormalGmailEmails))]
        public void SetNormalGmail(string email, string gmail)
        {
            var newEmail = EmailValidator.SetNormalGmail(email);

            Assert.Equal(gmail, newEmail);
        }

        [Theory]
        [MemberData(nameof(InvalidEmails))]
        [MemberData(nameof(InvalidDomains))]
        public void InvalidSetNormalGmail(string email)
        {
            Assert.Throws<FormatException>(() => EmailValidator.SetNormalGmail(email));
        }



    }
}