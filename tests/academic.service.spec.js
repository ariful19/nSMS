const academicService = require("../src/services/academicService");

describe("academicService.calculateGradeMetrics", () => {
  test("returns null metrics when score or max score missing", () => {
    expect(academicService.calculateGradeMetrics(null, 100)).toEqual({
      percentage: null,
      letter: null,
    });
    expect(academicService.calculateGradeMetrics(50, null)).toEqual({
      percentage: null,
      letter: null,
    });
  });

  test("calculates percentage and letter grade for valid inputs", () => {
    expect(academicService.calculateGradeMetrics(45, 50)).toEqual({
      percentage: 90,
      letter: "A",
    });
    expect(academicService.calculateGradeMetrics(78, 100)).toEqual({
      percentage: 78,
      letter: "C",
    });
  });

  test("guards against invalid numeric ranges", () => {
    expect(academicService.calculateGradeMetrics(-5, 50)).toEqual({
      percentage: 0,
      letter: "F",
    });
    expect(academicService.calculateGradeMetrics(30, 0)).toEqual({
      percentage: null,
      letter: null,
    });
  });
});
