namespace Staples
module Staples = 
    let openingStaples = ['('; '{'; '[']
    let closingStaples = [')'; '}'; ']']

    let areCorrectStaples expression =
        let rec recAreCorrectStaples expression checkList =  
            match expression with 
            | [] -> List.isEmpty checkList
            | x :: tail -> 
                if List.contains x openingStaples then 
                    recAreCorrectStaples tail (x :: checkList)
                elif List.contains x closingStaples then 
                    match checkList with 
                    | [] -> false
                    | char :: ls -> 
                        if (List.tryFindIndex (fun i -> i = x) closingStaples) = (List.tryFindIndex (fun i -> i = char) openingStaples) then recAreCorrectStaples tail ls
                        else false
                else 
                    recAreCorrectStaples tail checkList
        recAreCorrectStaples expression []

        

         