namespace GymApp.ApiService.Features.Quizzes.Data;

public static class RoutineQuiz
{
    public static Quiz Instance { get; } = new([
        new("¿Cuántos días a la semana puedes entrenar?", ["4", "5", "6"]),
        new("¿Cuánto tiempo puedes dedicar a cada sesión de entrenamiento?", ["Menos de 45 min", "45 min a 1 hora", "Más de 1 hora"]),
        new("¿Cuál es tu nivel actual de experiencia en entrenamiento?", ["Principiante (menos de 6 meses)", "Intermedio (6 meses a 2 años)", "Avanzado (más de 2 años)"]),
        new("¿Qué tipo de equipo tienes disponible para entrenar?", ["Gimnasio completo", "Mancuernas en casa", "Solo el peso corporal"]),
        new("¿Que tan intensamente te gustaría entrenar?", ["Todo al fallo", "Cerca del fallo", "¿Que es el fallo?"]),
        new("¿Qué tan exigente te gustaría que fuera tu entrenamiento?", ["Ligero", "Moderado", "Intenso"]),
        new("¿Que rango de repeticiones prefieres?", ["Altas", "Medias", "Bajas"]),
    ]);
}
