using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FlashCardApp.Models;

namespace FlashCardApp.Views
{
    public partial class FlashcardReview : Window
    {
        private readonly List<Flashcard> _flashcards;
        private int _currentIndex = 0;
        private bool _showingAnswer = false;

        public FlashcardReview(List<Flashcard> flashcards, string topicTitle)
        {
            InitializeComponent();
            _flashcards = flashcards.ToList();
            TopicTitleText.Text = $"Review: {topicTitle}";
            ShowFlashcard();
        }

        private void ShowFlashcard()
        {
            if (_flashcards.Count == 0)
            {
                FlashcardContent.Text = "No flashcards available.";
                return;
            }

            var current = _flashcards[_currentIndex];
            FlashcardContent.Text = _showingAnswer ? current.Answer : current.Question;
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            _showingAnswer = !_showingAnswer;
            ShowFlashcard();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_flashcards.Count == 0) return;
            _currentIndex = (_currentIndex + 1) % _flashcards.Count;
            _showingAnswer = false;
            ShowFlashcard();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_flashcards.Count == 0) return;
            _currentIndex = (_currentIndex - 1 + _flashcards.Count) % _flashcards.Count;
            _showingAnswer = false;
            ShowFlashcard();
        }

        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            var rng = new Random();
            _flashcards.Sort((a, b) => rng.Next(-1, 2));
            _currentIndex = 0;
            _showingAnswer = false;
            ShowFlashcard();
        }
    }
}
