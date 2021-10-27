using System;
using System.Linq;
using EF.Data;
using EF.Entities;
using EF.Entities.Enums;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ContactsBookDbContext())
            {
                var human = new Human() {Name = "Test", Surname = "Test", Patronymic = "Test"};
                context.Humans.Add(human);

                var phone = new Phone() {Number = "+380634475396", Opeator = Operator.Lifecell};
                context.Phones.Add(phone);

                context.SaveChanges();
            }

            using (var context = new ContactsBookDbContext())
            {
                var human = context.Humans.FirstOrDefault(h =>
                    h.Name.Equals("Test") && h.Surname.Equals("Test") && h.Patronymic.Equals("Test"));
                var phone = context.Phones.FirstOrDefault(p =>
                    p.Number.Equals("+380634475396"));

                var contact = new Contact()
                {
                    HumanId = human.Id,
                    Human = human,
                    PhoneNumber = phone.Number,
                    Phone = phone,
                    Note = "Description"
                };
                context.Contacts.Add(contact);

                context.SaveChanges();
            }

            using (var context = new ContactsBookDbContext())
            {
                foreach (var human in context.Humans)
                {
                    Console.WriteLine(human.Id + " => " + human.Name + " " + human.Surname + " " + human.Patronymic);
                }

                Console.WriteLine();

                foreach (var phone in context.Phones)
                {
                    Console.WriteLine(phone.Number + " => " + phone.Opeator);
                }

                Console.WriteLine();

                foreach (var contact in context.Contacts)
                {
                    Console.WriteLine(contact.HumanId + " => " + contact.Human.Name + " " + contact.Human.Surname + " " + contact.Human.Patronymic + " : " + 
                                      contact.PhoneNumber + " (" + contact.Phone.Opeator + ")");
                }
            }
        }
    }
}