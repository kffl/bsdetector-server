using NUnit.Framework;
using BSDetector.Analysis.Smells.AstSmells;

namespace BSDetector.Tests.Smells
{
    [TestFixture]
    public class LongMethodTest
    {
        private LongMethod smell;
        private ProcessingAstSmell detector;

        [SetUp]
        public void Setup()
        {
            smell = new LongMethod();
            detector = new ProcessingAstSmell(smell);
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestPos()
        {
            detector.code = @"function showChapter(chapter, tutorial) {
                if (chapter.orderIndex == 1)
                {
                    this.redirectToRoot()
                }
                let higherChapters = chapter.items.filter(item => item.higher)
                let lowerTutorials = chapter.items.filter(item => !item.higher)
                let title = chapter.title
                let description = chapter.description
                if (chapter.lowerItem)
                {
                    this.nextPath = this.getChapterPath(this.tutorial, chapter.lowerItem)
                    this.nextLink = this.nextPath
                }
                else if (!!tutorial.pathId && !!lowerTutorials)
                {
                    this.nextPath = this.getTutorialPath(lowerTutorials[0])
                    this.nextLink = this.nextPath
                }
                else if (chapter.last && !tutorial.footLinks.length > 0)
                    this.nextPath = tutorial.footLinks.url
                if (higherChapters.length === 0)
                    this.prevLink = this.getChapterPath(tutorial, higherChapters)
                else if (!!tutorial.pathId && tutorial.higherItem)
                {
                    let higherTutorial = tutorial.higherItem
                    lastChapter = higherTutorial.chapters[higherTutorial.chapters.length - 1]
          
                    if (lastChapter.orderIndex == 1)
                        this.prevLink = this.getTutorialPath(higherTutorial)
                    else
                        this.prevLink = this.getChapterPath(higherTutorial, higherChapters)
                }

                this.storeProps = {
                    checkableType: 'Chapter',
                    checkableId: chapter.id,
                    checkboxes: this.signedIn ? currentUser.getCheckboxesFor(chapter) : []
                }
                this.setStore()
                this.modal = this.getModalChapter(chapter) || this.getModalTutorial(tutorial)
            }";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(1));
        }

        [Test]
        public void ExcessivelyShortIdentifiersTestNeg()
        {
            detector.code = @"function showChapter(chapter, tutorial) {
                if (chapter.orderIndex == 1)
                {
                    this.redirectToRoot()
                }
                let higherChapters = chapter.items.filter(item => item.higher)
                let lowerTutorials = chapter.items.filter(item => !item.higher)
                let title = chapter.title
                let description = chapter.description
                if (chapter.lowerItem)
                {
                    this.nextPath = this.getChapterPath(this.tutorial, chapter.lowerItem)
                    this.nextLink = this.nextPath
                }
                else if (!!tutorial.pathId && !!lowerTutorials)
                {
                    this.nextPath = this.getTutorialPath(lowerTutorials[0])
                    this.nextLink = this.nextPath
                }
                else if (chapter.last && !tutorial.footLinks.length > 0)
                    this.nextPath = tutorial.footLinks.url
                if (higherChapters.length === 0)
                    this.prevLink = this.getChapterPath(tutorial, higherChapters)
                else if (!!tutorial.pathId && tutorial.higherItem)
                {
                    let higherTutorial = tutorial.higherItem
                    lastChapter = higherTutorial.chapters[higherTutorial.chapters.length - 1]
          
                    if (lastChapter.orderIndex == 1)
                        this.prevLink = this.getTutorialPath(higherTutorial)
                    else
                        this.prevLink = this.getChapterPath(higherTutorial, higherChapters)
                }
                this.storeProps = {
                    checkableType: 'Chapter',
                    checkableId: chapter.id,
                    checkboxes: this.signedIn ? currentUser.getCheckboxesFor(chapter) : []
                }
                this.setStore()
                this.modal = this.getModalChapter(chapter) || this.getModalTutorial(tutorial)
            }";
            detector.ASTAnalysis();
            Assert.That(smell.Occurrences.Count, Is.EqualTo(0));
        }
    }
}
