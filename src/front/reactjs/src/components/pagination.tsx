import { useEffect, useState } from "react";

interface IProps {
  totalPages: number;
  currentPage: number;
  changeCurrentPage: (newPage: number) => void;
}
function Pagination({ totalPages, currentPage, changeCurrentPage }: IProps) {
  const [_currentPage, setCurrentPage] = useState(0);

  useEffect(() => {
    setCurrentPage(currentPage);
  }, [currentPage]);

  return (
    <span>
      <a
        href="#"
        onClick={() => {
          setCurrentPage(1);
          changeCurrentPage(1);
        }}
      >
        [ &lt;&lt; ]
      </a>
      <a
        href="#"
        onClick={() => {
          if (_currentPage > 1) {
            setCurrentPage(_currentPage - 1);
            changeCurrentPage(_currentPage - 1);
          }
        }}
      >
        [ &lt; ]
      </a>
      {totalPages &&
        Array.from({ length: totalPages }, (_, i) => (
          <a
            key={i}
            href="#"
            onClick={() => {
              setCurrentPage(i + 1);
              changeCurrentPage(i + 1);
            }}
          >
            {_currentPage === i + 1 ? `[ ${i + 1}* ]` : `[ ${i + 1} ]`}
          </a>
        ))}
      <a
        href="#"
        onClick={() => {
          if (_currentPage < totalPages) {
            setCurrentPage(_currentPage + 1);
            changeCurrentPage(_currentPage + 1);
          }
        }}
      >
        [ &gt; ]
      </a>
      <a
        href="#"
        onClick={() => {
          setCurrentPage(totalPages);
          changeCurrentPage(totalPages);
        }}
      >
        [ &gt;&gt; ]
      </a>
    </span>
  );
}

export { Pagination };
