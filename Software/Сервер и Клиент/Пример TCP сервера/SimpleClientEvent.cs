using System;
using System.Net;

namespace SimpleAsyncServer
{

    /// <summary>
    /// Предоставляет универсальный метод, позволяющий произвести отключение клиента.
    /// </summary>
    /// <param name="sender">Экземпляр класса, инициировавший событие с клиентом.</param>
    /// <param name="e">Параметры произошедшего с клиентом события.</param>
    public delegate void SimpleClientEventDelegate(object sender, SimpleClientEventArgs e);

    /// <summary>
    /// Класс предоставляющий параметры произошедшего с клиентом события.
    /// </summary>
    public class SimpleClientEventArgs : EventArgs
    {

        /// <summary>
        /// Возвращает причину отключения клиента.
        /// </summary>
        public string Cause { get; private set; }

        /// <summary>
        /// Инициирует экземпляр класса DataTransferEventArgs.
        /// </summary>
        /// <param name="cause">Причина отключения клиента.</param>
        public SimpleClientEventArgs(string cause)
        {
            Cause = cause;
        }
        
        /// <summary>
        /// Возвращает текстовое представление информации о произошедшем событии.
        /// </summary>
        /// <returns>Текстовое представление информации о произошедшем событии.</returns>
        public override string ToString()
        {
            return string.Format(" отключен, так как {0}.", Cause);
        }

    }

}