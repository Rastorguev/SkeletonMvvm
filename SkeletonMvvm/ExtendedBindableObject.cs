using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace SkeletonMvvm
{
    public abstract class ExtendedBindableObject : BindableObject
    {
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            var lambdaExpression = (LambdaExpression) expression;
            if (lambdaExpression.Body is UnaryExpression expressionBody)
            {
                var body = expressionBody;
                operand = (MemberExpression) body.Operand;
            }
            else
            {
                operand = (MemberExpression) lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}