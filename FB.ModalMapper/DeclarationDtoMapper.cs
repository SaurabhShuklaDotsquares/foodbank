using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public class DeclarationDtoMapper
    {
        public static List<DeclarationDto> DeclarationMap(List<Declaration> declarations)
        {
            var declarationsList = new List<DeclarationDto>();
            foreach (var declarationsItem in declarations)
            {
                declarationsList.Add(new DeclarationDto
                {
                    PersonId= declarationsItem.PersonId,
                    DeclarationDate = declarationsItem.DateDeclarationSigned,
                    ValidForm = declarationsItem.DateDeclarationValidFrom,
                    ValidTo = declarationsItem.DateDeclarationValidTo
                });
            }
            return declarationsList;
        }
    }
}
