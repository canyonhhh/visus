class EmployeesController < ApplicationController
  before_action :set_employee, only: %i[ show edit update destroy ]

  def index
    @employees = Employee.all
  end

  def show
  end

  def new
    @employee = Employee.new
  end

  def edit
  end

  def create
    @employee = Employee.new(employee_params)

    if @employee.save
      redirect_to @employee, notice: "Employee was successfully created."
    else
      render :new, status: :unprocessable_entity
    end
  end

  def update
    if @employee.update(employee_params)
      redirect_to @employee, notice: "Employee was successfully updated.", status: :see_other
    else
      render :edit, status: :unprocessable_entity
    end
  end

  def destroy
    @employee.destroy!

    redirect_to employees_path, notice: "Employee was successfully destroyed.", status: :see_other
  end

  private

    def set_employee
      @employee = Employee.find(params.expect(:id))
    end

    def employee_params
      params.require(:employee).permit(:name, :email, :role, :business_id, :password, :password_confirmation)
    end
end
