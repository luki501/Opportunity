using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Opportunity.Model
{
    public interface IDialogService
    {
        void ShowError(Exception Error, string Title);
        void ShowError(string Message, string Title);
        void ShowInfo(string Message, string Title);
        void ShowMessage(string Message, string Title);
        bool ShowQuestion(string Message, string Title);
        void ShowWarning(string Message, string Title);
    }

    public class DialogService : IDialogService
    {
        public void ShowError(Exception Error, string Title)
        {
            MessageBox.Show(Error.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(Exception Error)
        {
            MessageBox.Show(Error.ToString(), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(string Error)
        {
            MessageBox.Show(Error, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(string Message, string Title)
        {
            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string Message, string Title)
        {
            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowInfo(string Message)
        {
            ShowInfo(Message, "komunikat");
        }

        public void ShowMessage(string Message, string Title)
        {
            MessageBox.Show(Message, Title, MessageBoxButton.OK);
        }

        public bool ShowQuestion(string Message, string Title)
        {
            return MessageBox.Show(Message, Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public void ShowWarning(string Message, string Title)
        {
            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void ShowWarning(string Message)
        {
            MessageBox.Show(Message, "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
