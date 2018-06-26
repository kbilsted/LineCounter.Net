# LineCounter.Net
A library to do sensible line counting and generating "shields" for Github readme's.

Project statistics:
<!--start-->
[![Stats](https://img.shields.io/badge/Code_lines-386-ff69b4.svg)]()
[![Stats](https://img.shields.io/badge/Doc_lines-2-ff69b4.svg)]()
<!--end-->



## Sensible line counting
What is "sensible line counting"? The idea is to count lines of code in such a way that 

  * Only lines with semantic value are counted.
  * Various coding styles for the same code should roughly yield the same line counting.
  * Take into account "you get what you measure".
    
Why is that important? Because we want an estimate of size of code irregardles of coding style. For example

    class Foo { // example 1
        public void Bar() {
            if(moons == 9) {
                 Planet = Pluto;
            } else {
                 Planet = Mars;
            }
        }
        public string Another() {
            Console.WriteLine("*")
        }
    }

Should roughly yield the same lines of code as the less compact style

    class Foo // example 2
    {
        public void Bar() 
        {
            if(moons == 9) 
            {
                 Planet = Pluto;
            } 
            else 
            {
                 Planet = Mars;
            }
        }
        
        public string Another() 
        {
            Console.WriteLine("*")
        }
    }


Example 1 takes up 12 lines of code while example 2 takes 19 lines -- 58% longer! Linecounter will report 8 lines of code for both code examples.



## You get what you measure

The theory "you get what you measure" means exactly that. By whatever metric you measure, people will adapt seeking the easiest way to perform within the confinements of the measurements. 
E.g. when you praise programmers for the number of lines of code, they'll start making long-winded code. 
I've withnessed this first-hand a couple of times with outsourcing. 

I'm well aware that this is far from perfect with respect to only counting semantic lines, but its a start.. and another reason I wrote this was to test on live data some string-algorithm optimizations I will blog about on http://firstclassthoughts.co.uk/ some day :-)


