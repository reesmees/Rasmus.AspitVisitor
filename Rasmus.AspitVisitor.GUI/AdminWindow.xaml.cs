using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Rasmus.AspitVisitor.DataAccess.EF;
using Rasmus.AspitVisitor.Business;
using System.Data.Entity;

namespace Rasmus.AspitVisitor.GUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public DataHandler handler;
        public AdminWindow(DataHandler handler)
        {
            InitializeComponent();
            this.handler = handler;
            //handler.DB.Questionaires.Load();
            //handler.DB.Questions.Load();
            //handler.DB.MultipleChoiceAnswers.Load();
            //dgrdQuestionaires.ItemsSource = handler.DB.Questionaires.Local;
            //dgrdQuestionaires.Items.Refresh();
        }

        private void btnAddQuestionaire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbxAdminName.Text))
                {
                    Admin a = handler.DB.Admins.Where(ad => ad.name == tbxAdminName.Text).SingleOrDefault();
                    a.Questionaires.Add(new Questionaire { Admin = a, isActive = false });
                    handler.DB.SaveChanges();
                    dgrdQuestionaires.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl! {Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void btnSetActiveQuestionaire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Questionaire questionaire = (Questionaire)dgrdQuestionaires.SelectedItem;
                if (questionaire != null)
                {
                    if (!questionaire.isActive)
                    {
                        foreach (Questionaire q in handler.DB.Questionaires)
                        {
                            q.isActive = false;
                        }
                        handler.DB.Questionaires.Where(q => q.ID == questionaire.ID).Single().isActive = true;
                        handler.DB.SaveChanges();
                    }
                }
                dgrdQuestionaires.Items.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Der skete en fejl! {Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void dgrdQuestionaires_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dgrdMultipleChoiceAnswers.ItemsSource = null;
                Questionaire questionaire = (Questionaire)dgrdQuestionaires.SelectedItem;
                dgrdQuestions.ItemsSource = handler.DB.Questions.Local.Where(q => q.Questionaire.ID == questionaire.ID);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl! {Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void btnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbxQuestionBody.Text) && tbxQuestionBody.Text != "Indtast spørgsmålstekst her")
                {
                    Questionaire questionaire = (Questionaire)dgrdQuestionaires.SelectedItem;
                    handler.DB.Questionaires.Where(q => q.ID == questionaire.ID).SingleOrDefault().Questions.Add(new Question { questionBody = tbxQuestionBody.Text, isMultipleChoice = (bool)chbxIsMultipleChoice.IsChecked });
                    handler.DB.SaveChanges();
                    dgrdQuestions.ItemsSource = questionaire.Questions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl! {Environment.NewLine}{Environment.NewLine}{ex.Message}");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            handler.DB.Questionaires.Load();
            handler.DB.Questions.Load();
            handler.DB.MultipleChoiceAnswers.Load();
            dgrdQuestionaires.ItemsSource = handler.DB.Questionaires.Local;
            dgrdQuestionaires.Items.Refresh();
        }
    }
}
