using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPKTOnline.Management
{
    #region [Enum]

    public enum SemesterEnum { Semesterone = 1, Semestertwo = 2, semesterthree = 3  };
    public enum ExamPaperStatusEnum { ChuaThi = 1, DangThi = 2, Dathi = 3 };
    public enum ExamTypeEnum { GiuaKy = 1, CuoiKy = 2 };
    public enum QuestionTypeEnum {TrueFalse = 1, MultipleChoice = 2, MergeColumn = 3, ImcompleteSentence = 4};
    public enum FormatExamEnum { Toeic = 1, Default = 2 };
    public enum AnswerTypeEnum { One = 1, Many = 2 };
    public enum ToeicQuestionType { Picture = 1, QuestionAndResponse = 2, ShortConversation = 3, ShortTalk = 4, IncompleteSentences = 5, IncompleteTexts = 6, ReadingComprehension = 7 };
    public enum SortExamStatus { Name = 1, StudentCode = 2, IsTesting = 3, IsCompleted = 4, Absent = 5 };
    public enum DifficultLevelEnum { Easy = 1, Medium = 2, MediumDifficult = 3, Difficult = 4 };
    public enum QuestionSkillEnum { Listening = 1, Speaking = 2, Reading = 3, Writing = 4 };
    public enum ExamBankTypeEnum { Examination = 1, ExaminationBank = 2 };
    public enum LanguageEnum { English = 1, VietNamese = 2 };
    #endregion
}
