namespace Staples
module Staples = 
    let openingStaples = ['('; '{'; '[']
    let closingStaples = [')'; '}'; ']']

    let rec areCorrectStaples expression checkList =  
        match expression with 
        | [] -> true
        | x :: tail -> 
            if List.contains x openingStaples then areCorrectStaples tail (x :: checkList)
            elif List.contains x closingStaples then 
                match checkList with 
                | [] -> false
                | char :: ls -> 
                    if (List.tryFindIndex (fun i -> i = x) closingStaples) = (List.tryFindIndex (fun i -> i = char) openingStaples) then areCorrectStaples tail ls
                    else false
            else areCorrectStaples tail checkList

        

         
